using AiLingua.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace AiLingua.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(string id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task AddAsync(T entity);
        void Update(T entity);
        void DeleteAsync(T entity);
        Task<T?>  GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetByPropertyIncludesAsync(
    Expression<Func<T, bool>> predicate,
    params Expression<Func<T, object>>[] includes);

    }
}
