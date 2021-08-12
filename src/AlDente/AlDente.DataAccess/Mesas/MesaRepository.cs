using AlDente.DataAccess.Core;
using AlDente.Entities.Mesas;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Mesas
{
    public class MesaRepository : MyBaseRepository<Mesa>, IMesaRepository
    {
        public MesaRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

        public async Task<IEnumerable<Mesa>> GetMesasDisponibles(DateTime fecha, int turnoId)
        {
            return await this.ExecuteQueryAsync("dbo.GetMesasDisponibles", new { Fecha = fecha, TurnoId = turnoId }, System.Data.CommandType.StoredProcedure);
        }

    }
}
