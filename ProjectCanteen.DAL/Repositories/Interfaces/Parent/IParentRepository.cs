using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Parent
{
    public interface IParentRepository : IBaseRepository<Entities.Parent>
    {
        Task<Entities.Parent?> GetByIdAsync(int id);
    }
}
