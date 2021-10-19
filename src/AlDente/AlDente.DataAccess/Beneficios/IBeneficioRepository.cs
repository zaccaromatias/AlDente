using AlDente.DataAccess.Core;
using AlDente.Entities.Beneficios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Beneficios
{
    public interface IBeneficioRepository : IRepository<Beneficio>
    {
        Task<IEnumerable<Beneficio>> GetActivosByCliente(int clienteId);
        Task RemoveBeneficiosActivos(int clienteId);
    }
}
