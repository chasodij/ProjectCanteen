using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void BindContext(ProjectCanteenDBContext dbContext);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sorting = null);

        Task<(IEnumerable<TEntity> entities, int totalCount)> GetRangeAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sorting = null,
            int? page = null,
            int? pageSize = null);

        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task AttachAsync(TEntity entity);
    }
}
