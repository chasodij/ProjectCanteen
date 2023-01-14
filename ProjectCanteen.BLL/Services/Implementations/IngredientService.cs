using AutoMapper;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IProjectCanteenUoW _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientService(IProjectCanteenUoW unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateIngredientAsync(CreateIngredientDTO createIngredientDTO, int? workerId = null)
        {
            var ingredient = _mapper.Map<Ingredient>(createIngredientDTO);

            if (workerId != null)
            {
                ingredient.Canteen = await _unitOfWork.CanteenRepository
                    .GetFirstOrDefaultAsync(x => x.CanteenWorkers.Any(worker => worker.Id == workerId));
            }

            await _unitOfWork.IngredientRepository.AttachAsync(ingredient);
            await _unitOfWork.IngredientRepository.CreateAsync(ingredient);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);

            if (ingredient != null)
            {
                await _unitOfWork.IngredientRepository.DeleteAsync(ingredient);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> IsIngredientExistWithIdAsync(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);

            return ingredient != null;
        }

        public async Task UpdateIngredientAsync(UpdateIngredientDTO ingredientDTO)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);

            await _unitOfWork.IngredientRepository.UpdateAsync(ingredient);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<(IEnumerable<FullIngredientDTO> ingredients, int totalCount)> GetIngredientsAsync(int page, int pageSize, int? workerId = null)
        {
            var (ingredients, totalCount) = await _unitOfWork.IngredientRepository
                    .GetRangeAsync(page: page, pageSize: pageSize,
                        predicate: x => (workerId == null && x.Canteen == null) ||
                                         (x.Canteen != null && x.Canteen.Id == workerId));

            return (_mapper.Map<IEnumerable<FullIngredientDTO>>(ingredients), totalCount);
        }
    }
}
