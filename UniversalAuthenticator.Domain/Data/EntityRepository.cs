using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Domain.Paging;

namespace UniversalAuthenticator.Domain.Data
{
    /// <summary>
    /// Class Entity Repository.
    /// Implements the <see cref="UniversalAuthenticator.Domain.Data.IRepository{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="UniversalAuthenticator.Domain.Data.IRepository{T}" />

    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private readonly UniversalAuthDbContext _context;

        public EntityRepository(UniversalAuthDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach(var includ in includeExpressions)
            {
                set = set.Include(includ);
            }

            T result = await set.FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetFirstAsync()
        {
            return await _context.Set<T>().FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstAsync(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach(var include in includeExpressions)
            {
                set = set.Include(include);
            }

            T result = await set.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAllAsync(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach(var include in includeExpressions)
            {
                set = set.Include(include);
            }
            return await set.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();
            foreach(var include in includeExpressions)
            {
                set = set.Include(include);
            }
            IReadOnlyList<T> results = await set.AsNoTracking().Where(predicate).ToListAsync();
            return results;
        }

        public async Task<PagedResult<T>> ListWithPagingAsync(int currentPage, int pageSize, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = _context.Set<T>();

            foreach(var include in includeExpressions)
            {
                set = set.Include(include);
            }

            var result= await set.AsNoTracking().Where(predicate).GetPagedAsync(currentPage, pageSize);
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }


    }
}
