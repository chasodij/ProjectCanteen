using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProjectCanteen.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace ProjectCanteen.DAL.Repositories.Implementations.NewFolder
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ProjectCanteenDBContext DbContext { get; set; }
        protected Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> DefaultInclude { get; set; }

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
            await Task.Run(() => DbContext.Remove(entity));
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Set<TEntity>().Update(entity));
        }
        public async Task AttachAsync(TEntity entity)
        {
            await Task.Run(() => DbContext.Set<TEntity>().Attach(entity));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sorting = null)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            include ??= DefaultInclude;

            if (include != null)
            {
                query = include(query);
            }

            if (sorting != null)
            {
                query = sorting(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            include ??= DefaultInclude;

            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<(IEnumerable<TEntity> entities, int totalCount)> GetRangeAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sorting = null,
            int? page = null,
            int? pageSize = null)
        {
            var query = this.DbContext.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            include ??= DefaultInclude;

            if (include != null)
            {
                query = include(query);
            }

            if (sorting != null)
            {
                query = sorting(query);
            }

            query = query.AsNoTracking();

            var TotalRecords = await query.CountAsync();

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((int)(pageSize * (page - 1)))
                    .Take((int)pageSize);
            }

            return (query, TotalRecords);
        }
    }
}
