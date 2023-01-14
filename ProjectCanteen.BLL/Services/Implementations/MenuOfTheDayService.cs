using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class MenuOfTheDayService : IMenuOfTheDayService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public MenuOfTheDayService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseDTO> CreateMenuAsync(CreateMenuOfTheDayDTO createMenuOfTheDayDTO, int workerId)
        {
            var menu = _mapper.Map<MenuOfTheDay>(createMenuOfTheDayDTO);

            var canteen = await _unitOfWork.CanteenRepository
               .GetFirstOrDefaultAsync(x => x.CanteenWorkers.Any(worker => worker.Id == workerId));

            if (canteen == null)
            {
                return new BaseResponseDTO
                {
                    Success = false,
                    Errors = new List<string> { "There are no canteen you working for" }
                };
            }

            TimeSpan span = createMenuOfTheDayDTO.Day.Subtract(DateTime.UtcNow);

            if (span.TotalHours <= canteen.MinHoursToCreateMenu)
            {
                menu.IsCreatedOrUpdatedLate = true;
            }

            menu.Canteen = canteen;


            await _unitOfWork.MenuOfTheDayRepository.AttachAsync(menu);
            await _unitOfWork.MenuOfTheDayRepository.CreateAsync(menu);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDTO
            {
                Success = true
            };
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            var menu = await _unitOfWork.MenuOfTheDayRepository.GetByIdAsync(id);

            if (menu != null)
            {
                await _unitOfWork.MenuOfTheDayRepository.DeleteAsync(menu);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<FullDishDTO>> GetMenuDishesForStudent(int studentId, DateTime date, int canteenId)
        {
            var result = new List<FullDishDTO>();

            var menu = await _unitOfWork.MenuOfTheDayRepository
                .GetFirstOrDefaultAsync(x => x.Day.Date == date.Date && x.Canteen.Id == canteenId,
                include: func => func.Include(x => x.Dishes)
                                     .ThenInclude(x => x.IngredientInDishes)
                                     .ThenInclude(x => x.Ingredient)
                                     .ThenInclude(x => x.DietaryRestrictions));

            var studentRestrictions = (await _unitOfWork.StudentRepository.GetFirstOrDefaultAsync(x => x.Id == studentId)).DietaryRestrictions;

            foreach (var dish in menu.Dishes)
            {
                var dishRestrictions = dish.IngredientInDishes.SelectMany(x => x.Ingredient.DietaryRestrictions).Distinct();

                foreach (var studentRestriction in studentRestrictions)
                {
                    if (!dishRestrictions.Any(x => x.Id == studentRestriction.Id))
                    {
                        continue;
                    }
                }

                result.Add(_mapper.Map<FullDishDTO>(dish));
            }
            return result;
        }

        public async Task<(IEnumerable<FullMenuOfTheDayDTO> menus, int totalCount)> GetMenusAsync(int page, int pageSize, int workerId)
        {
            var (menus, totalCount) = await _unitOfWork.MenuOfTheDayRepository
                    .GetRangeAsync(page: page, pageSize: pageSize,
                        predicate: x => x.Canteen.CanteenWorkers.Any(x => x.Id == workerId),
                        include: func => func.Include(x => x.Dishes).ThenInclude(x => x.MenuSection));

            return (_mapper.Map<IEnumerable<FullMenuOfTheDayDTO>>(menus), totalCount);
        }

        public async Task<bool> IsMenuExistWithIdAsync(int id)
        {
            var menu = await _unitOfWork.MenuOfTheDayRepository.GetByIdAsync(id);

            return menu != null;
        }

        public async Task UpdateMenuAsync(UpdateMenuOfTheDayDTO menuOfTheDayDTO)
        {
            var menu = _mapper.Map<MenuOfTheDay>(menuOfTheDayDTO);

            await _unitOfWork.MenuOfTheDayRepository.UpdateAsync(menu);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
