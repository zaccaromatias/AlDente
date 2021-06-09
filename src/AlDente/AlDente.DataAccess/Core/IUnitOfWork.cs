using System.Data.Common;
using System.Data.SqlClient;

namespace AlDente.DataAccess.Core
{
    public interface IUnitOfWork<TDbConnection>
    {
        TDbConnection Connection { get; }
        DbTransaction Transaction { get; }
        void Begin();
        void Rollback();
        void Commit();
    }

    public interface IUnitOfWork : IUnitOfWork<SqlConnection>
    { }
}
