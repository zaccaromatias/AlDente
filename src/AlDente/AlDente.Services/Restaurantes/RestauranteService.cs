using AlDente.Contracts.Restaurantes;
using AlDente.Contracts.Turnos;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Restaurantes;
using AlDente.Entities.Restaurantes;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Id = restauranteDto.Id,
                    Descripcion = restauranteDto.Descripcion,
                    Direccion = restauranteDto.Direccion,
                    Telefono = restauranteDto.Telefono,
                    UrlMenu = restauranteDto.UrlMenu
                });
            });
        }

        public async Task<string> GetUrlMenu()
        {
            return await Try(async () =>
            {
                return (await this.restauranteRepository.GetByIdAsync(this.unitOfWork.RestauranteId)).UrlMenu;
            });
        }

        public async Task<IEnumerable<DiaLaborableDTO>> GetDiasLaborables()
        {
            //TODO:CAMBIAR POR DATA REAL
            return await this.Try(async () =>
            {
                var jueves = new DiaLaborableDTO
                {
                    DiaDeLaSemana = 4,
                    HoraInicio = TimeSpan.FromHours(20),
                    HoraFin = TimeSpan.FromHours(0),
                };

                jueves.Turnos.Add(new TurnoDTO
                {
                    Id = 1,
                    HoraInicio = TimeSpan.FromHours(20),
                    HoraFin = TimeSpan.FromHours(22),
                });
                jueves.Turnos.Add(new TurnoDTO
                {
                    Id = 2,
                    HoraInicio = TimeSpan.FromHours(22),
                    HoraFin = TimeSpan.FromHours(0),
                });
                var viernes = new DiaLaborableDTO
                {
                    DiaDeLaSemana = 5,
                    HoraInicio = TimeSpan.FromHours(20),
                    HoraFin = TimeSpan.FromHours(2),
                };
                viernes.Turnos.Add(new TurnoDTO
                {
                    Id = 1,
                    HoraInicio = TimeSpan.FromHours(20),
                    HoraFin = TimeSpan.FromHours(22),
                });
                viernes.Turnos.Add(new TurnoDTO
                {
                    Id = 2,
                    HoraInicio = TimeSpan.FromHours(22),
                    HoraFin = TimeSpan.FromHours(0),
                });
                var dias = new List<DiaLaborableDTO>()
                {
                    jueves,viernes
                };
                return await Task.FromResult(dias);
            });
        }
    }
}
