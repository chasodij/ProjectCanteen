using ProjectCanteen.BLL.DTOs.Ingredient;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDTO>> GetIngredientsAsync();
        Task CreateIngredientAsync(CreateIngredientDTO createIngredientDTO);
        Task UpdateIngredientAsync(IngredientDTO ingredientDTO);
        Task<bool> IsIngredientExistWithIdAsync(int id);
        Task<bool> DeleteIngredientAsync(int id);
    }
}
