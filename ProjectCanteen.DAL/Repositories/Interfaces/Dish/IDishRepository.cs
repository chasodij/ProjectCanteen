using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Dish
{
    public interface IDishRepository : IBaseRepository<Entities.Dish>
    {
        Task<Entities.Dish?> GetByIdAsync(int id);
    }
}
