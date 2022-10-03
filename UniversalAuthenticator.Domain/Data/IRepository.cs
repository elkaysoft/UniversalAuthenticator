using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Domain.Paging;

namespace UniversalAuthenticator.Domain.Data
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(long id);
        Task<T> GetByAsync(ISpecification<T> spec);

        Task<T> GetByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);

        Task<T> GetFirstAsync();
        Task<T> GetFirstAsync(params Expression<Func<T, object>>[] includeExpressions);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<List<T>> ListAllAsync(params Expression<Func<T, object>>[] includeExpressions);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);

        Task<PagedResult<T>> ListWithPagingAsync(int currentPage, int pageSize, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);
                
        Task AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
    }
}
