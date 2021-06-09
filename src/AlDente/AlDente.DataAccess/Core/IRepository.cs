using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Attach(IUnitOfWork unitOfWork);
        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);
        int AddAll(IEnumerable<TEntity> entities);
        int Delete(object id);
        Task<int> DeleteAsync(object id);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        TResult Merge<TResult>(TEntity entity);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
    }
}
