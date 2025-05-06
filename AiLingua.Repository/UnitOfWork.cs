using AiLingua.Core;
using AiLingua.Core.Entities;
using AiLingua.Core.Repositories.Contract;
using AiLingua.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;

        }
        // New method to handle eager loading and GetByIdIncludesAsync
        public async Task<TEntity?> GetByIdIncludesAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            // Apply eager loading for each included navigation property
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Assuming "Id" is the key field
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();


    }
}
