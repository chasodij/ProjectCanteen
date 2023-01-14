using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

            if (order != null)
            {
                await _unitOfWork.OrderRepository.DeleteAsync(order);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<UpdateOrderDTO>> GetOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UpdateOrderDTO>>(orders);
        }

        public async Task<(IEnumerable<FullOrderDTO> orders, int totalCount)> GetOrdersAsync(int page, int pageSize, int workerId)
        {
            var (orders, totalCount) = await _unitOfWork.OrderRepository
                    .GetRangeAsync(page: page, pageSize: pageSize,
                        predicate: x => x.OrderItems.First().Dish.Canteen.CanteenWorkers.Any(x => x.Id == workerId));

            return (_mapper.Map<IEnumerable<FullOrderDTO>>(orders), totalCount);
        }

        public async Task<bool> IsOrderExistWithIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

            return order != null;
        }

        public async Task UpdateOrderAsync(UpdateOrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);

            await _unitOfWork.OrderRepository.UpdateAsync(order);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BaseResponseDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, CanteenWorker canteenWorker)
        {
            return await CreateOrderAsRoleAsync(createOrderDTO, canteenWorker: canteenWorker);
        }

        public async Task<BaseResponseDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, Parent parent)
        {
            return await CreateOrderAsRoleAsync(createOrderDTO, parent: parent);
        }

        private async Task<BaseResponseDTO> CreateOrderAsRoleAsync(CreateOrderDTO createOrderDTO, CanteenWorker? canteenWorker = null, Parent? parent = null)
        {
            var order = await MapOrderFromDTO(createOrderDTO);

            var canCreate = new BaseResponseDTO { Success = false };

            if (canteenWorker != null)
            {
                canCreate = await CanCanteenWorkerCreateOrderAsync(order, canteenWorker);
            }
            else if (parent != null)
            {
                canCreate = await CanParentCreateOrder(order, parent);
            }

            var restrictionsResult = await CanStudentWithRestrictionsEatDishes(order);

            canCreate += restrictionsResult;

            if (!canCreate.Success)
            {
                return canCreate;
            }

            order.OrderStatus = await _unitOfWork.OrderRepository.GetStatusByName(OrderStatuses.Ordered);
            order.OrderTime = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDTO
            {
                Success = true
            };
        }

        private async Task<Order> MapOrderFromDTO(CreateOrderDTO createOrderDTO)
        {
            var order = _mapper.Map<Order>(createOrderDTO);

            await _unitOfWork.OrderRepository.AttachAsync(order);

            order.OrderItems = new List<OrderItem>();

            foreach (var item in createOrderDTO.OrderItems)
            {
                var dishFromDb = await _unitOfWork.DishRepository.GetFirstOrDefaultAsync(x => x.Id == item.DishId,
                    include: func => func.Include(x => x.IngredientInDishes).ThenInclude(x => x.Ingredient).ThenInclude(x => x.DietaryRestrictions));

                if (dishFromDb == null)
                {
                    throw new Exception();
                }

                order.OrderItems.Add(
                    new OrderItem
                    {
                        DishId = dishFromDb.Id,
                        DishPrice = dishFromDb.Price,
                        Portions = item.Portions
                    });
            }

            var menu = await _unitOfWork.MenuOfTheDayRepository
                .GetFirstOrDefaultAsync(x => x.Id == createOrderDTO.MenuOfTheDayId,
                    include: func => func.Include(y => y.Canteen));

            order.MenuOfTheDay = menu;

            return order;
        }

        private async Task<BaseResponseDTO> CanCanteenWorkerCreateOrderAsync(Order order, CanteenWorker canteenWorker)
        {

            if (DateTime.UtcNow.Date != order.MenuOfTheDay.Day.Date)
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You can't create order for this student now" }
                };
            }

            if (canteenWorker.Canteen.Id != order.MenuOfTheDay.Canteen.Id)
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You can't create order for this student" }
                };
            }


            if (order.MenuOfTheDay.Canteen.MaxStudentDebt > order.Student.CurrentDebt)
            {
                decimal price = order.OrderItems.Sum(x => x.DishPrice * x.Portions);

                if (order.MenuOfTheDay.Canteen.MaxStudentDebt <= order.Student.CurrentDebt + price)
                {
                    return new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "The student has insufficient funds on the debt account" }
                    };
                }

                order.Student.CurrentDebt += price;
            }
            else
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "The student has insufficient funds on the debt account" }
                };
            }

            order.IsOrderedLate = true;

            return new BaseResponseDTO
            {
                Success = true
            };
        }

        private async Task<BaseResponseDTO> CanParentCreateOrder(Order order, Parent parent)
        {
            TimeSpan span = order.MenuOfTheDay.Day.Date.Subtract(DateTime.UtcNow);

            if (span.TotalHours <= order.MenuOfTheDay.Canteen.MinHoursToOrder)
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You can't create order on this date now" }
                };
            }

            if (!parent.Children.Any(x => x.Id == order.Student.Id))
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "You can't create order for this student" }
                };
            }

            order.PurchaserId = parent.Id;

            return new BaseResponseDTO
            {
                Success = true
            };
        }

        private async Task<BaseResponseDTO> CanStudentWithRestrictionsEatDishes(Order order)
        {
            var orderDishes = new List<Dish>();

            var menuDishes = (await _unitOfWork.MenuOfTheDayRepository
                    .GetFirstOrDefaultAsync(x => x.Id == order.MenuOfTheDay.Id)).Dishes;

            foreach (var item in order.OrderItems)
            {
                var dish = await _unitOfWork.DishRepository.GetFirstOrDefaultAsync(dish => dish.Id == item.DishId,
                    include: func => func.Include(x => x.IngredientInDishes)
                                         .ThenInclude(x => x.Ingredient)
                                         .ThenInclude(x => x.DietaryRestrictions));

                if (!menuDishes.Any(x => x.Id == dish.Id))
                {
                    return new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "There are no dish with such id in selected menu" }
                    };
                }

                if (dish != null)
                {
                    orderDishes.Add(dish);
                }
            }

            var orderIngredients = orderDishes.SelectMany(x => x.IngredientInDishes).
                Select(x => x.Ingredient).Distinct();

            var orderRestrictions = orderIngredients.SelectMany(x => x.DietaryRestrictions).Distinct();

            var studentRestrictions = (await _unitOfWork.StudentRepository
                .GetFirstOrDefaultAsync(x => x.Id == order.Student.Id))?
                .DietaryRestrictions.Distinct().ToList() ?? new List<DietaryRestriction>();

            foreach (var restriction in studentRestrictions)
            {
                if (!orderRestrictions.Any(x => x.Id == restriction.Id))
                {
                    return new BaseResponseDTO
                    {
                        Success = false,
                        Errors = new List<string> { "You can't create order with these dietary restrictions for this student" }
                    };
                }
            }
            return new BaseResponseDTO
            {
                Success = true
            };
        }

        public async Task<FullOrdersOfTheDayDTO> GetOrdersOfTheDayAsync(DateTime date, CanteenWorker canteenWorker)
        {
            var orders = await _unitOfWork.OrderRepository
                .GetAllAsync(x => x.MenuOfTheDay.Canteen == canteenWorker.Canteen &&
                                  x.MenuOfTheDay.Day.Date == date.Date);

            return _mapper.Map<FullOrdersOfTheDayDTO>(orders);
        }

        public async Task<(string fullStudentName, bool isSuccess, List<int> ordersId, int canteenId)> RequestOrders(string tagId, User userTerminal)
        {
            var canteen = await _unitOfWork.CanteenRepository.GetFirstOrDefaultAsync(x => x.TerminalId == userTerminal.Id);

            if (canteen == null)
            {
                return (string.Empty, false, new List<int>(), 0);
            }

            var student = await _unitOfWork.StudentRepository.GetFirstOrDefaultAsync(x => x.TagId == tagId);
            
            if (student == null)
            {
                return (string.Empty, false, new List<int>(), 0);
            }

            var orders = await _unitOfWork.OrderRepository
                .GetAllAsync(x => x.MenuOfTheDay.Day.Date == DateTime.UtcNow.Date &&
                                             x.MenuOfTheDay.Canteen.Id == canteen.Id &&
                                             x.Student.Id == student.Id &&
                                             x.OrderStatus.Name == OrderStatuses.Ordered);

            if (orders.Count() == 0)
            {
                return ("No orders for today", true, new List<int>(), canteen.Id);
            }

            foreach (var order in orders)
            {
                _unitOfWork.OrderRepository.ChangeStatusTo(order.Id, OrderStatuses.Requested);
            }

            await _unitOfWork.SaveChangesAsync();

            return (student.User.LastName + " " + student.User.FirstName + " " + student.User.Patronymic,
                true, orders.Select(x => x.Id).ToList(), canteen.Id);
        }

        public async Task<bool> CompleteOrder(int orderId, CanteenWorker worker)
        {
            var order = await _unitOfWork.OrderRepository
                .GetFirstOrDefaultAsync(x => x.Id == orderId && 
                                             worker.Canteen.Id == x.MenuOfTheDay.Canteen.Id &&
                                             x.OrderStatus.Name == OrderStatuses.Requested);

            if (order == null)
            {
                return false;
            }

            _unitOfWork.OrderRepository.ChangeStatusTo(orderId, OrderStatuses.Completed);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
