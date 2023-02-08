using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.CanteenWorker;

namespace ProjectCanteen.DAL.Repositories.Implementations.CanteenWorker
{
    public class CanteenWorkerRepository : BaseRepository<Entities.CanteenWorker>, ICanteenWorkerRepository
    {
        public CanteenWorkerRepository()
        {
            DefaultInclude = func => func.Include(worker => worker.User)
                .Include(worker => worker.Canteen);
        }

        public override async Task UpdateAsync(Entities.CanteenWorker entity)
        {
            var connectedWorker = await DbContext.Set<Entities.CanteenWorker>().FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (connectedWorker == null)
            {
                throw new Exception();
            }

            var connectedUser = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == connectedWorker.UserId);

            if (connectedUser == null)
            {
                throw new Exception();
            }

            connectedUser.FirstName = entity.User.FirstName;
            connectedUser.LastName = entity.User.LastName;
            connectedUser.Patronymic = entity.User.Patronymic;

            DbContext.Entry(connectedWorker).CurrentValues.SetValues(entity);

            DbContext.Entry(connectedWorker).Reference(x => x.User).IsModified = false;

            await base.UpdateAsync(connectedWorker);
        }

        public async Task<Entities.CanteenWorker?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
