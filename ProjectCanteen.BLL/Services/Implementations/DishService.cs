using AutoMapper;
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

        public async Task CreateDishAsync(CreateDishDTO createDishDTO)
        {
            var dish = _mapper.Map<Dish>(createDishDTO);

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

        public async Task<IEnumerable<DishDTO>> GetDishesAsync()
        {
            var dishes = await _unitOfWork.DishRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DishDTO>>(dishes);
        }

        public async Task<bool> IsDishExistWithIdAsync(int id)
        {
            var dish = await _unitOfWork.DishRepository.GetByIdAsync(id);

            return dish != null;
        }

        public async Task UpdateDishAsync(DishDTO dishDTO)
        {
            var dish = _mapper.Map<Dish>(dishDTO);

            await _unitOfWork.DishRepository.UpdateAsync(dish);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
