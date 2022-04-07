using AdvancedProgramming.Data;
using AdvancedProgramming.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdvancedProgramming.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private EmployeeContext dbContext;
        private readonly EmployeeContext _dbContext;

        public Repository(EmployeeContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("Context was not supplied");
            _dbSet = _dbContext.Set<T>();
        }

 

        public IQueryable<T> GetAllIgnoreQueryFilter()
        {
            IQueryable<T> query = _dbSet.IgnoreQueryFilters();
            return query.AsNoTracking();
        }

        public IQueryable<T> GetAllTracking(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public IQueryable<T> GetAll(ISpecification<T> spec)
        {
            return ApplySpecification(spec);
        }

        public (int, IQueryable<T>) GetAllWithPaginationCount(ISpecification<T> spec)
        {
            return ApplySpecificationWithPaginationCount(spec);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetAll(filter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.AsNoTracking().Where(predicate);

        public T Insert(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(Guid id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(Guid userId, List<Guid> roles)
        {
            foreach (var item in roles)
            {
                T entityToDelete = _dbSet.Find(userId, item);
                Delete(entityToDelete);
            }
        }

        public async void DeleteAsync(Guid id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public void Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsNoTracking(), spec);
        }

        private (int, IQueryable<T>) ApplySpecificationWithPaginationCount(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQueryWithPaginationCount(_dbContext.Set<T>().AsNoTracking(), spec);
        }

    }
}
