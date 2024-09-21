using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private CatalogContext _db;
        private DbSet<T> _dbSet;

        public Repository(CatalogContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }


        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> CreateBulk(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
            return entities;
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            T? entity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<T> RemoveAsync(Guid id)
        {
            T? entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return null;
            }

            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
