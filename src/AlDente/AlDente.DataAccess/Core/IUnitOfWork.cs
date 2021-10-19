using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Core
{
    public interface IUnitOfWork<TDbConnection>
    {
        TDbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void Begin();
        void Rollback();
        void Commit();

        Task<TResult> TryWithTransact<TResult>(Func<Task<TResult>> action);
    }

    public interface IUnitOfWork : IUnitOfWork<SqlConnection>
    {
        public int RestauranteId { get; }
        public string URL { get; }
    }
}
