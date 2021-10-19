using Microsoft.Extensions.Options;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Core
{
    public abstract class MyBaseRepository<TEntity> : BaseRepository<TEntity, SqlConnection>, IRepository<TEntity>
        where TEntity : class
    {
        private IUnitOfWork unitOfWork;

        private DbTransaction transaction => unitOfWork?.Transaction;

        public MyBaseRepository(IOptions<AppSettings> settings)
           : base(settings.Value.ConnectionString)
        {
        }

        public int Add(TEntity entity)
        {
            return Insert<int>(entity, transaction: transaction);

        }
        public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await InsertAsync<int>(entity, transaction: transaction, cancellationToken: cancellationToken);
        }

        public int AddAll(IEnumerable<TEntity> entities)
        {
            return InsertAll(entities, transaction: transaction);
        }

        public async Task<int> AddAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return await base.InsertAllAsync(entities, transaction: transaction, cancellationToken: cancellationToken);
        }

        public void Attach(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int Delete(object id)
        {
            return Delete(id, transaction: transaction);
        }

        public async Task<int> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync(id, transaction: transaction, cancellationToken: cancellationToken);
        }

        public int Delete(TEntity entity)
        {
            return Delete<TEntity>(entity, transaction: transaction);
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync<TEntity>(entity, transaction: transaction, cancellationToken: cancellationToken);
        }

        public TResult Merge<TResult>(TEntity entity)
        {
            return Merge<TResult>(entity, transaction: unitOfWork?.Transaction);
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return Query(where, transaction: transaction);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken)
        {
            return await QueryAsync(where, transaction: transaction, cancellationToken: cancellationToken);
        }
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken)
        {
            return await CountAsync(where, transaction: transaction, cancellationToken: cancellationToken);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return base.QueryAll(transaction: transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await base.QueryAllAsync(transaction: transaction, cancellationToken: cancellationToken);
        }

        public int Update(TEntity entity)
        {
            return Update(entity, entity, transaction: transaction);
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await UpdateAsync(entity, transaction: transaction, cancellationToken: cancellationToken);
        }

        public TEntity GetById(object id)
        {
            return Query(id, transaction: transaction).FirstOrDefault();
        }

        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return (await QueryAsync(id, transaction: transaction, cancellationToken: cancellationToken)).FirstOrDefault();
        }
    }

}
