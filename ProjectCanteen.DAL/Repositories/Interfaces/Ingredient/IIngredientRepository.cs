using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Ingredient
{
    public interface IIngredientRepository : IBaseRepository<Entities.Ingredient>
    {
        Task<Entities.Ingredient?> GetByIdAsync(int id);
    }
}
