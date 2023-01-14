using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class DishService : IDishService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public DishService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDishAsync(CreateDishDTO createDishDTO, int workerId)
        {
            var dish = _mapper.Map<Dish>(createDishDTO);

            var connectedWorker = await _unitOfWork.CanteenWorkerRepository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == workerId,
                include: x => x.Include(worker => worker.Canteen));

            if (connectedWorker == null)
            {
                throw new Exception();
            }

            dish.Canteen = connectedWorker.Canteen;

            await _unitOfWork.DishRepository.AttachAsync(dish);
            await _unitOfWork.DishRepository.CreateAsync(dish);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteDishAsync(int id)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(id);

            if (dish != null)
            {
                await _unitOfWork.DishRepository.DeleteAsync(dish);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<(IEnumerable<FullDishDTO> dishes, int totalCount)> GetDishesAsync(int page, int pageSize, int workerId)
        {
            var (dishes, totalCount) = await _unitOfWork.DishRepository.GetRangeAsync(page: page, pageSize: pageSize,
                                   predicate: x => x.Canteen.CanteenWorkers.Any(x => x.Id == workerId));

            return (_mapper.Map<IEnumerable<FullDishDTO>>(dishes), totalCount);
        }

        public async Task<bool> IsDishExistWithIdAsync(int id)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(id);

            return dish != null;
        }

        public async Task UpdateDishAsync(UpdateDishDTO dishDTO)
        {
            var dish = _mapper.Map<Dish>(dishDTO);

            await _unitOfWork.DishRepository.UpdateAsync(dish);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
