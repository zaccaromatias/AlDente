using AlDente.DataAccess.Core;
using AlDente.Entities.EstadosClientes;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.EstadosClientes
{
    public class EstadoClienteRepository : MyBaseRepository<EstadoCliente>, IEstadoClienteRepository
    {

        public EstadoClienteRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

    }
}
