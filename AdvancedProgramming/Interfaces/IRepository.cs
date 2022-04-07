using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdvancedProgramming.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllIgnoreQueryFilter();

        IQueryable<T> GetAllTracking(Expression<Func<T, bool>> filter = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);

        IQueryable<T> GetAll(ISpecification<T> spec);

        (int, IQueryable<T>) GetAllWithPaginationCount(ISpecification<T> spec);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<IReadOnlyCollection<T>> ListAsync(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        T Insert(T entity);

        void InsertRange(IEnumerable<T> entities);

        T Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Delete(Guid userId, List<Guid> roles);

        void Delete(Guid id);

        void Delete(T entity);

        void DeleteAsync(Guid id);

        void DeleteRange(IEnumerable<T> entities);
    }
}
