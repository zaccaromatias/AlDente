using AlDente.Contracts.Core;
using AlDente.DataAccess.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public class BaseService
    {
        protected IUnitOfWork unitOfWork;
        protected Dictionary<string, string> CustomValidations { get; } = new Dictionary<string, string>();
        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResult> Try<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return await func();
            }
            catch (System.Exception ex)
            {
                throw Handle(ex);
            }
        }

        public async Task Try(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (System.Exception ex)
            {
                throw Handle(ex);
            }
        }

        private Exception Handle(Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                switch (sqlException.Number)
                {
                    case 2627:  // Unique constraint error
                    case 547:   // Constraint check violation
                    case 2601:  // Duplicated key row error
                                // Constraint violation exception
                                // A custom exception of yours for concurrency issues
                        throw new DomainException(GetCustomMessage(sqlException));
                    default:
                        // A custom exception of yours for other DB issues
                        throw exception;
                }
            }
            throw exception;
        }

        private string GetCustomMessage(SqlException sqlException)
        {
            if (!this.CustomValidations.Any())
                return sqlException.Message;
            KeyValuePair<string, string>? customValidation = this.CustomValidations
                .FirstOrDefault(x => sqlException.Message.Contains(x.Key));
            return customValidation?.Value ?? sqlException.Message;

        }
    }
}
