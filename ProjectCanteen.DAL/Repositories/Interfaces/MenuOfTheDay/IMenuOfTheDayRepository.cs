using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.MenuOfTheDay
{
    public interface IMenuOfTheDayRepository : IBaseRepository<Entities.MenuOfTheDay>
    {
        Task<Entities.MenuOfTheDay?> GetByIdAsync(int id);
    }
}
