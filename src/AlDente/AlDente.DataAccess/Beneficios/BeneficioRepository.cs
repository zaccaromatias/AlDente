using AlDente.DataAccess.Core;
using AlDente.Entities.Beneficios;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.DataAccess.Beneficios
{
    public class BeneficioRepository : MyBaseRepository<Beneficio>, IBeneficioRepository
    {
        public BeneficioRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }


        public async Task<IEnumerable<Beneficio>> GetActivosByCliente(int clienteId)
        {

            var beneficiosNoAplicados = await QueryAsync(x => x.ClienteId == clienteId && !x.Aplicado);
            return beneficiosNoAplicados.Where(x => !x.Expiro);
        }
        public async Task RemoveBeneficiosActivos(int clienteId)
        {
            var actives = await GetActivosByCliente(clienteId);
            if (actives.Count() > 0)
                await DeleteAllAsync<int>(actives.Select(x => x.Id));
        }
    }
}
