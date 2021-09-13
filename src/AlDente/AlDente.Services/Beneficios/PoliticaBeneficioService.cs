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
        private IPoliticaBeneficioRepository politicaBeneficioRepository;

        public PoliticaBeneficioService(IUnitOfWork unitOfWork, IPoliticaBeneficioRepository pbRepository)
            : base(unitOfWork)
        {
            pbRepository.Attach(this.unitOfWork);
            this.politicaBeneficioRepository = pbRepository;
        }
        public async Task<IEnumerable<PoliticaBeneficioDTO>> GetAll()
        {
            var politicas = await politicaBeneficioRepository.GetAllAsync();
            return politicas.Select(x => new PoliticaBeneficioDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                TipoBeneficioId = x.TipoBeneficioId,
                NumeroMinimo = x.NumeroMinimo,
                Periodo = x.Periodo,
                EstadoReservaId = (EstadosDeUnaReserva)x.EstadoReservaId,
                RestauranteId = x.RestauranteId 
            });
        }
        public async Task Create(PoliticaBeneficioDTO politicaBeneficioDTO)
        {
            await Try(async () =>
            {
                await politicaBeneficioRepository.AddAsync(new PoliticaBeneficio
                {
                    Descripcion = politicaBeneficioDTO.Descripcion,
                    TipoBeneficioId = politicaBeneficioDTO.TipoBeneficioId,
                    NumeroMinimo =  politicaBeneficioDTO.NumeroMinimo,
                    Periodo = politicaBeneficioDTO.Periodo,
                    EstadoReservaId = (int)politicaBeneficioDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId


                });
            });
        }

        public async Task Delete(int id)
        {
            await politicaBeneficioRepository.DeleteAsync(id);
        }

        public async Task Update(PoliticaBeneficioDTO politicaBeneficioDTO)
        {
            await Try(async () =>
            {
                await politicaBeneficioRepository.UpdateAsync(new PoliticaBeneficio
                {
                    Id = politicaBeneficioDTO.Id,
                    Descripcion = politicaBeneficioDTO.Descripcion,
                    TipoBeneficioId = politicaBeneficioDTO.TipoBeneficioId,
                    NumeroMinimo = politicaBeneficioDTO.NumeroMinimo,
                    Periodo = politicaBeneficioDTO.Periodo,
                    EstadoReservaId = (int)politicaBeneficioDTO.EstadoReservaId,
                    RestauranteId = this.unitOfWork.RestauranteId
                });
            });
        }

    }
}
