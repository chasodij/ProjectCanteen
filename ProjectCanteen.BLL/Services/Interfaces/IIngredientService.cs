using ProjectCanteen.BLL.DTOs.Ingredient;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<(IEnumerable<FullIngredientDTO> ingredients, int totalCount)> GetIngredientsAsync(int page, int pageSize, int? workerId = null);
        Task CreateIngredientAsync(CreateIngredientDTO createIngredientDTO, int? workerId = null);
        Task UpdateIngredientAsync(UpdateIngredientDTO ingredientDTO);
        Task<bool> IsIngredientExistWithIdAsync(int id);
        Task<bool> DeleteIngredientAsync(int id);
    }
}
