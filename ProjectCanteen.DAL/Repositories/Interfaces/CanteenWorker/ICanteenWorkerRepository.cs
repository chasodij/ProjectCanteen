using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.CanteenWorker
{
    public interface ICanteenWorkerRepository : IBaseRepository<Entities.CanteenWorker>
    {
        Task<Entities.CanteenWorker?> GetByIdAsync(int id);
    }
}
