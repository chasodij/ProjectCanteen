using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace ProjectCanteen.DAL.Repositories.Implementations.NewFolder
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ProjectCanteenDBContext DbContext { get; set; }

        public void BindContext(ProjectCanteenDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Set<TEntity>().Add(entity));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Entry(entity).State = EntityState.Deleted);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                var items = await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
                return items;
            }
            return await DbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Set<TEntity>().Update(entity));
        }
        public async Task AttachAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Set<TEntity>().Attach(entity));
        }
    }
}
