using Microsoft.Extensions.Options;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace AlDente.DataAccess.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppSettings settings;
        private SqlConnection connection;
        private DbTransaction transaction;

        public SqlConnection Connection => EnsureConnection();

        public DbTransaction Transaction => transaction;

        private SqlConnection EnsureConnection() =>
            connection = connection ?? new SqlConnection(settings.ConnectionString);

        public UnitOfWork(IOptions<AppSettings> settings)
        {
            this.settings = settings.Value;
        }

        public void Begin()
        {
            if (transaction != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction while the existing other one is still open.");
            }
            var connection = EnsureConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to commit.");
            }
            using (transaction)
            {
                transaction.Commit();
            }
            transaction = null;
        }

        public void Rollback()
        {
            if (transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction to rollback.");
            }
            using (transaction)
            {
                transaction.Rollback();
            }
            transaction = null;
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
