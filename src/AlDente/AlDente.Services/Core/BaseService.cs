using AlDente.DataAccess.Core;
using System;
using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public class BaseService
    {
        protected IUnitOfWork unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResult> Try<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                unitOfWork.Begin();
                var result = await func();
                unitOfWork.Commit();
                return result;
            }
            catch (System.Exception ex)
            {
                unitOfWork.Rollback();
                throw ex;
            }
        }
    }
}
