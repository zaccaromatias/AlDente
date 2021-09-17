using AlDente.Contracts.Core;
using AlDente.Contracts.Sanciones;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Sanciones;
using AlDente.Entities.Sanciones;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Contracts.Reservas;

namespace AlDente.Services.Sanciones
{
    public class PoliticaSancionService : BaseService, IPoliticaSancionService
    {
        private IPoliticaSancionRepository _politicaSancionRepository;
        private ITipoSancionRepository _tipoSancionRepository;

        public PoliticaSancionService(IUnitOfWork unitOfWork, IPoliticaSancionRepository politicaSancionRepository, ITipoSancionRepository tipoSancionRepository)
            : base(unitOfWork)
        {
            _politicaSancionRepository = politicaSancionRepository;
            _tipoSancionRepository = tipoSancionRepository;
            _politicaSancionRepository.Attach(unitOfWork);
            _tipoSancionRepository.Attach(unitOfWork);

        }
        public async Task<IEnumerable<PoliticaSancionDTO>> GetAll()
        {
            var result = await _politicaSancionRepository.GetAllAsync();
            var tasks = await Task.WhenAll(result.Select(async x => await MapToDTO(x)));
            return tasks;
        }

        public async Task<PoliticaSancionDTO> MapToDTO(PoliticaSancion x)
        {
            var dto = new PoliticaSancionDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                NumeroMaximo = x.NumeroMaximo,
                Periodo = x.Periodo,
                EstadoReservaId = (EstadosDeUnaReserva)x.EstadoReservaId,
                RestauranteId = x.RestauranteId
            };
            dto.TipoSancion = await GetTipoBeneficio(x.TipoSancionId);
            return dto;
        }

        private async Task<TipoSancionDTO> GetTipoBeneficio(int tipoSancionId)
        {
            var tipoSancion = await _tipoSancionRepository.GetByIdAsync(tipoSancionId); ;
            return new TipoSancionDTO
            {
                Id = tipoSancion.Id,
                Descripcion = tipoSancion.Descripcion,
                Codigo = tipoSancion.Codigo,
                DiasSuspension = tipoSancion.DiasSuspension
            };
        }
        public async Task Create(PoliticaSancionDTO politicaSancionDTO)
        {
            await Try(async () =>
            {
                await _politicaSancionRepository.AddAsync(new PoliticaSancion
                {
                    Descripcion = politicaSancionDTO.Descripcion,
                    TipoSancionId = politicaSancionDTO.TipoSancion.Id,
                    NumeroMaximo = politicaSancionDTO.NumeroMaximo,
                    Periodo = politicaSancionDTO.Periodo,
                    EstadoReservaId = (int)politicaSancionDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId


                });
            });
        }

        public async Task Delete(int id)
        {
            await _politicaSancionRepository.DeleteAsync(id);
        }

        public async Task Update(PoliticaSancionDTO politicaSancionDTO)
        {
            await Try(async () =>
            {
                await _politicaSancionRepository.UpdateAsync(new PoliticaSancion
                {
                    Id = politicaSancionDTO.Id,
                    Descripcion = politicaSancionDTO.Descripcion,
                    TipoSancionId = politicaSancionDTO.TipoSancion.Id,
                    NumeroMaximo = politicaSancionDTO.NumeroMaximo,
                    Periodo = politicaSancionDTO.Periodo,
                    EstadoReservaId = (int)politicaSancionDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId
                });
            });
        }

    }
}