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

        public async Task CreateIngredientAsync(CreateIngredientDTO createIngredientDTO)
        {
            var ingredient = _mapper.Map<Ingredient>(createIngredientDTO);

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

        public async Task<IEnumerable<IngredientDTO>> GetIngredientsAsync()
        {
            var ingredients = await _unitOfWork.IngredientRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<IngredientDTO>>(ingredients);
        }

        public async Task<bool> IsIngredientExistWithIdAsync(int id)
        {
            var ingredient = await _unitOfWork.IngredientRepository.GetByIdAsync(id);

            return ingredient != null;
        }

        public async Task UpdateIngredientAsync(IngredientDTO ingredientDTO)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);

            //await _unitOfWork.IngredientRepository.AttachAsync(ingredient);

            await _unitOfWork.IngredientRepository.UpdateAsync(ingredient);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
