using AlDente.DataAccess.Core;
using AlDente.Entities.Clientes;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Clientes
{
    public class ClienteRepository : MyBaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }
    }
}
