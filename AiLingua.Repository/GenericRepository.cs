using AiLingua.Core.Entities;
using AiLingua.Core.Repositories.Contract;
using AiLingua.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T?> GetByIdIncludesAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            // Apply eager loading for each included navigation property
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Assuming "Id" is the key field
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<List<T>> GetByPropertyIncludesAsync(
    Expression<Func<T, bool>> predicate,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            // Apply eager loading for each included navigation property
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Apply filtering by property
            return await query.Where(predicate).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetByIdAsync(string id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        => await _dbContext.AddAsync(entity);
        public void Update(T entity)
         => _dbContext.Update(entity);


        public void DeleteAsync(T entity)
        => _dbContext.Remove(entity);
        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

    }
}
