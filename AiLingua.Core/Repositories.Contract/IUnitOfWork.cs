using AiLingua.Core.Entities;
using AiLingua.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AiLingua.Core
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
        Task<TEntity?> GetByIdIncludesAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

    }
}
