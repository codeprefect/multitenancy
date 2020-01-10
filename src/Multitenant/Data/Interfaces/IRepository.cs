using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTenant.Data.Interfaces
{
    public interface IRepository<TContext> : IReadOnlyRepository
    {
        TEntity Add<TEntity>(TEntity entity, string createdBy = null)
        where TEntity : class, IEntity;

        IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities, string createdBy = null)
        where TEntity : class, IEntity;

        TEntity Update<TEntity>(TEntity entity, string modifiedBy = null)
        where TEntity : class, IEntity;

        IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities, string modifiedBy = null)
        where TEntity : class, IEntity;

        void Delete<TEntity, Tin>(Tin id, string deletedBy = null)
        where TEntity : class, IEntity;

        void DeleteRange<TEntity, Tin>(IEnumerable<Tin> id, string deletedBy = null)
        where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity, string deletedBy = null)
        where TEntity : class, IEntity;
        void DeleteRange<TEntity>(IEnumerable<TEntity> entity, string deletedBy = null)
        where TEntity : class, IEntity;

        Task<int> SaveAsync();
    }
}
