using AlDente.Contracts.Core;
using AlDente.Contracts.Sanciones;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Sanciones;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Contracts.Reservas;
using AlDente.Entities.Sanciones;

namespace AlDente.Services.Sanciones
{
    public class PoliticaSancionService : BaseService, IPoliticaSancionService
    {
        private IPoliticaSancionRepository politicaSancionRepository;

        public PoliticaSancionService(IUnitOfWork unitOfWork, IPoliticaSancionRepository psRepository)
            : base(unitOfWork)
        {
            psRepository.Attach(this.unitOfWork);
            this.politicaSancionRepository = psRepository;
        }
        public async Task<IEnumerable<PoliticaSancionDTO>> GetAll()
        {
            var politicas = await politicaSancionRepository.GetAllAsync();
            return politicas.Select(x => new PoliticaSancionDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                TipoSancionId = x.TipoSancionId,
                NumeroMaximo = x.NumeroMaximo,
                Periodo = x.Periodo,
                EstadoReservaId = (EstadosDeUnaReserva)x.EstadoReservaId,
                RestauranteId = x.RestauranteId
            });
        }
        public async Task Create(PoliticaSancionDTO politicaSancionDTO)
        {
            await Try(async () =>
            {
                await politicaSancionRepository.AddAsync(new PoliticaSancion
                {
                    Descripcion = politicaSancionDTO.Descripcion,
                    TipoSancionId = politicaSancionDTO.TipoSancionId,
                    NumeroMaximo = politicaSancionDTO.NumeroMaximo,
                    Periodo = politicaSancionDTO.Periodo,
                    EstadoReservaId = (int)politicaSancionDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId


                });
            });
        }

        public async Task Delete(int id)
        {
            await politicaSancionRepository.DeleteAsync(id);
        }

        public async Task Update(PoliticaSancionDTO politicaSancionDTO)
        {
            await Try(async () =>
            {
                await politicaSancionRepository.UpdateAsync(new PoliticaSancion
                {
                    Id = politicaSancionDTO.Id,
                    Descripcion = politicaSancionDTO.Descripcion,
                    TipoSancionId = politicaSancionDTO.TipoSancionId,
                    NumeroMaximo = politicaSancionDTO.NumeroMaximo,
                    Periodo = politicaSancionDTO.Periodo,
                    EstadoReservaId = (int)politicaSancionDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId
                });
            });
        }

    }
}
