using AlDente.Contracts.Core;
using AlDente.Contracts.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Beneficios;
using AlDente.Entities.Beneficios;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Contracts.Reservas;

namespace AlDente.Services.Sanciones
{
    public class PoliticaBeneficioService : BaseService, IPoliticaBeneficioService
    {
        private IPoliticaBeneficioRepository _politicaBeneficioRepository;
        private ITipoBeneficioRepository _tipoBeneficioRepository;

        public PoliticaBeneficioService(IUnitOfWork unitOfWork, IPoliticaBeneficioRepository politicaBeneficioRepository, ITipoBeneficioRepository tipoBeneficioRepository)
            : base(unitOfWork)
        {
           _politicaBeneficioRepository = politicaBeneficioRepository;
           _tipoBeneficioRepository = tipoBeneficioRepository;
           _politicaBeneficioRepository.Attach(unitOfWork);
           _tipoBeneficioRepository.Attach(unitOfWork);

        }
        public async Task<IEnumerable<PoliticaBeneficioDTO>> GetAll()
        {
            var result = await _politicaBeneficioRepository.GetAllAsync();
            var tasks = await Task.WhenAll(result.Select(async x => await MapToDTO(x)));
            return tasks;
        }

        public async Task<PoliticaBeneficioDTO> MapToDTO(PoliticaBeneficio x)
        {
            var dto = new PoliticaBeneficioDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                NumeroMinimo = x.NumeroMinimo,
                Periodo = x.Periodo,
                EstadoReservaId = (EstadosDeUnaReserva)x.EstadoReservaId,
                RestauranteId = x.RestauranteId
            };
            dto.TipoBeneficio = await GetTipoBeneficio(x.TipoBeneficioId);
            return dto;
        }

        private async Task<TipoBeneficioDTO> GetTipoBeneficio(int tipoBeneficioId)
        {
            var tipoBeneficio = await _tipoBeneficioRepository.GetByIdAsync(tipoBeneficioId); ;
            return new TipoBeneficioDTO 
            {
                Id= tipoBeneficio.Id,
                Descripcion = tipoBeneficio.Descripcion,
                Codigo = tipoBeneficio.Codigo,
                Descuento = tipoBeneficio.Descuento
            };
        }
        public async Task Create(PoliticaBeneficioDTO politicaBeneficioDTO)
        {
            await Try(async () =>
            {
                await _politicaBeneficioRepository.AddAsync(new PoliticaBeneficio
                {
                    Descripcion = politicaBeneficioDTO.Descripcion,
                    TipoBeneficioId = politicaBeneficioDTO.TipoBeneficio.Id,
                    NumeroMinimo =  politicaBeneficioDTO.NumeroMinimo,
                    Periodo = politicaBeneficioDTO.Periodo,
                    EstadoReservaId = (int)politicaBeneficioDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId


                });
            });
        }

        public async Task Delete(int id)
        {
            await _politicaBeneficioRepository.DeleteAsync(id);
        }

        public async Task Update(PoliticaBeneficioDTO politicaBeneficioDTO)
        {
            await Try(async () =>
            {
                await _politicaBeneficioRepository.UpdateAsync(new PoliticaBeneficio
                {
                    Id = politicaBeneficioDTO.Id,
                    Descripcion = politicaBeneficioDTO.Descripcion,
                    TipoBeneficioId = politicaBeneficioDTO.TipoBeneficio.Id,
                    NumeroMinimo = politicaBeneficioDTO.NumeroMinimo,
                    Periodo = politicaBeneficioDTO.Periodo,
                    EstadoReservaId = (int)politicaBeneficioDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId
                });
            });
        }

    }
}
