using AlDente.DataAccess.Core;
using AlDente.Entities.Sanciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Sanciones
{
    public interface ISancionRepository : IRepository<Sancion>
    {
        Task<IEnumerable<Sancion>> GetActivosByCliente(int clienteId);
        Task RemoveSancionesActivas(int clienteId);
    }
}
