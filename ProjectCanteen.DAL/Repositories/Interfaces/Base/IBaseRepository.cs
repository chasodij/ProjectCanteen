using System.Linq.Expressions;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void BindContext(ProjectCanteenDBContext dbContext);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task AttachAsync(TEntity entity);
    }
}
