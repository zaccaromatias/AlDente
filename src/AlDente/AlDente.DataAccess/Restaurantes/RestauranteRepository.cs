using AlDente.DataAccess.Core;
using AlDente.Entities.Restaurantes;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

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
