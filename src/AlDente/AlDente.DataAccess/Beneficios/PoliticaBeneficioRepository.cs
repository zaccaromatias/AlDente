using AlDente.DataAccess.Core;
using AlDente.Entities.Beneficios;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Beneficios
{
    public class PoliticaBeneficioRepository : MyBaseRepository<PoliticaBeneficio>, IPoliticaBeneficioRepository
    {
        public PoliticaBeneficioRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }

    }
}