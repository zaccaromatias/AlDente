using AlDente.DataAccess.Core;
using AlDente.Entities.Sanciones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlDente.DataAccess.Sanciones
{
    public class SancionRepository : MyBaseRepository<Sancion>, ISancionRepository
    {
        ITipoSancionRepository _tipoSancionRepository;
        public SancionRepository(IOptions<AppSettings> settings, ITipoSancionRepository tipoSancionRepository)
            : base(settings)
        {
            _tipoSancionRepository = tipoSancionRepository;
        }

        public async Task<IEnumerable<Sancion>> GetActivosByCliente(int clienteId)
        {
            var todasLasSanciones = await QueryAsync(x => x.ClienteId == clienteId);
            var pepe = todasLasSanciones.ToAsyncEnumerable().WhereAwait(async x => await EstaActiva(x));
            return await pepe.ToListAsync();
        }

        private async Task<bool> EstaActiva(Sancion x)
        {
            var tipo = await _tipoSancionRepository.GetByIdAsync(x.TipoSancionId);
            var fechaHasta = x.FechaSansion.AddDays(tipo.DiasSuspension);
            return DateTime.Today < fechaHasta.Date;
        }

        public async Task RemoveSancionesActivas(int clienteId)
        {
            var actives = await GetActivosByCliente(clienteId);
            if (actives.Count() > 0)
                await DeleteAllAsync<int>(actives.Select(x => x.Id));
        }
    }
}
