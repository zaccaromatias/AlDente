using AlDente.DataAccess.Core;
using AlDente.Entities.Mesas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Mesas
{
    public interface IMesaRepository : IRepository<Mesa>
    {
        Task<IEnumerable<Mesa>> GetMesasDisponibles(DateTime fecha, int turnoId);
    }
}
