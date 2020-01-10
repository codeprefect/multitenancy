using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MultiTenant.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MultiTenant.Data.Repositories
{
    public class ReadOnlyRepository<TContext> : IReadOnlyRepository where TContext : DbContext
    {
        protected readonly TContext _context;

        public ReadOnlyRepository(TContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        where TEntity : class, IEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) query = orderBy(query);

            if (skip.HasValue) query = query.Skip(skip.Value);

            if (take.HasValue) query = query.Take(take.Value);

            return query;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual ValueTask<TEntity> GetByIdAsync<TEntity, Tin>(Tin id)
        where TEntity : class, IEntity
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).CountAsync();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
        where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).AnyAsync();
        }
    }
}
