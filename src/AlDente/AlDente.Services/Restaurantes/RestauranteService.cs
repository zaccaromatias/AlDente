using AlDente.Contracts.Restaurantes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Restaurantes;
using AlDente.Entities.Restaurantes;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlDente.Services.Restaurantes
{
    public class RestauranteService : BaseService, IRestauranteService
    {
        private IRestauranteRepository restauranteRepository;

        public RestauranteService(IUnitOfWork unitOfWork, IRestauranteRepository restauranteRepository)
            : base(unitOfWork)
        {
            restauranteRepository.Attach(this.unitOfWork);
            this.restauranteRepository = restauranteRepository;
        }


        public async Task<IEnumerable<RestauranteDTO>> GetAll()
        {
            var restaurantes = await restauranteRepository.GetAllAsync();
            return restaurantes.Select(x => new RestauranteDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
                UrlMenu = x.UrlMenu
            });
        }

        public async Task Create(RestauranteDTO restauranteDto)
        {
            await Try(async () =>
            {
                await restauranteRepository.AddAsync(new Restaurante
                {
                    Descripcion = restauranteDto.Descripcion,
                    Direccion = restauranteDto.Direccion,
                    Telefono = restauranteDto.Telefono,
                    UrlMenu = restauranteDto.UrlMenu
                });
            });
        }

        public async Task Delete(int id)
        {
            await restauranteRepository.DeleteAsync(id);
        }

        public async Task Update(RestauranteDTO restauranteDto)
        {
            await Try(async () =>
            {
                await restauranteRepository.UpdateAsync(new Restaurante
                {
                    Id= restauranteDto.Id, 
                    Descripcion = restauranteDto.Descripcion,
                    Direccion = restauranteDto.Direccion,
                    Telefono = restauranteDto.Telefono,
                    UrlMenu = restauranteDto.UrlMenu
                });
            });
        }
    }
}
