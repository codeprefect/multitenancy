using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiTenant.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MultiTenant.Data.Repositories
{
    public class Repository<TContext> : ReadOnlyRepository<TContext>, IRepository<TContext> where TContext : DbContext
    {
        public Repository(TContext context) : base(context) { }

        public virtual TEntity Add<TEntity>(TEntity entity, string createdBy = null) where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities, string createdBy = null) where TEntity : class, IEntity
        {
            IList<TEntity> list = new List<TEntity>();
            foreach (var entity in entities)
            {
                list.Add(Add(entity, createdBy));
            }
            return list;
        }

        public virtual TEntity Update<TEntity>(TEntity entity, string modifiedBy = null) where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Update(entity);
            }
            return entity;
        }

        public virtual IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities, string modifiedBy = null) where TEntity : class, IEntity
        {
            IList<TEntity> list = new List<TEntity>();
            foreach (var entity in entities)
            {
                list.Add(Update(entity, modifiedBy));
            }
            return list;
        }

        public virtual void Delete<TEntity, Tin>(Tin id, string deletedBy = null) where TEntity : class, IEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity, deletedBy);
        }

        public virtual void DeleteRange<TEntity, Tin>(IEnumerable<Tin> ids, string deletedBy = null) where TEntity : class, IEntity
        {
            foreach (var id in ids)
            {
                Delete<TEntity, Tin>(id, deletedBy);
            }
        }

        public virtual void Delete<TEntity>(TEntity entity, string deletedBy = null) where TEntity : class, IEntity
        {
            var dbSet = _context.Set<TEntity>();
            entity.Deleted = DateTime.UtcNow;
            entity.ModifiedBy = deletedBy;
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Update(entity);
            }
        }

        public virtual void DeleteRange<TEntity>(IEnumerable<TEntity> entities, string deletedBy = null) where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                Delete(entity, deletedBy);
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
