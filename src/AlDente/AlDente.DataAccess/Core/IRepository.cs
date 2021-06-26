using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Attach(IUnitOfWork unitOfWork);
        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        int AddAll(IEnumerable<TEntity> entities);
        int Delete(object id);
        Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        TResult Merge<TResult>(TEntity entity);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
