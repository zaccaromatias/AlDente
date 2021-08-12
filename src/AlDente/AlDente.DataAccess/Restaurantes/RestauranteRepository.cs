using AlDente.DataAccess.Core;
using AlDente.Entities.Restaurantes;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Restaurantes
{
    public class RestauranteRepository : MyBaseRepository<Restaurante>, IRestauranteRepository
    {
        public RestauranteRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }
    }
}
