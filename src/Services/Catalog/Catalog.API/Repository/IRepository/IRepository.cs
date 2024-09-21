using System.Linq.Expressions;

namespace Catalog.API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        Task<IEnumerable<T>> CreateBulk(IEnumerable<T> entities);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> RemoveAsync(Guid id);

        Task<T> UpdateAsync(T entity);
    }
}
