using AlDente.Contracts.Beneficios;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.Entities.Beneficios;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Beneficios
{
    public class BeneficioService : BaseService, IBeneficioService
    {
        IBeneficioRepository _beneficioRepository;
        ITipoBeneficioRepository _tipoBeneficioRepository;
        public BeneficioService(IUnitOfWork unitOfWork, IBeneficioRepository beneficioRepository, ITipoBeneficioRepository tipoBeneficioRepository)
            : base(unitOfWork)
        {
            _beneficioRepository = beneficioRepository;
            _tipoBeneficioRepository = tipoBeneficioRepository;
            _beneficioRepository.Attach(unitOfWork);
            _tipoBeneficioRepository.Attach(unitOfWork);
        }

        public async Task<IEnumerable<BeneficioDTO>> GetActiveByCliente(int clienteId)
        {
            return await this.Try<IEnumerable<BeneficioDTO>>(async () =>
            {
                var beneficios = await _beneficioRepository.GetActivosByCliente(clienteId);
                List<dynamic> resultados = new List<dynamic>();
                foreach (var item in beneficios)
                {
                    var tipo = await _tipoBeneficioRepository.GetByIdAsync(item.TipoBeneficioId);
                    resultados.Add(new { Beneficio = item, Tipo = tipo });
                }
                return resultados.Select(x => MapToDTO((Beneficio)x.Beneficio, (TipoBeneficio)x.Tipo));
            });

        }

        public async Task<BasicResultDTO> AplicarBeneficio(int beneficioId)
        {
            return await this.Try(async () =>
            {
                var beneficio = await _beneficioRepository.GetByIdAsync(beneficioId);
                if (beneficio == null)
                    return BasicResultDTO.Failled("No Existe Beneficio");
                if (beneficio.Aplicado)
                    return BasicResultDTO.Failled("Beneficio ya Aplicado");
                if (!beneficio.FechaPedidoDeAplicacion.HasValue)
                    return BasicResultDTO.Failled("El Beneficio no fue Solicitado");
                if (string.IsNullOrEmpty(beneficio.Codigo))
                    return BasicResultDTO.Failled("El Beneficio no fue Solicitado");
                if (beneficio.Expiro)
                    return BasicResultDTO.Failled("El Beneficio ya Expiro");
                beneficio.FechaAplicado = DateTime.Now;
                beneficio.Aplicado = true;
                await _beneficioRepository.UpdateAsync(beneficio);
                return BasicResultDTO.Success($"El {beneficio.Codigo} se ha aplicado.");
            });
        }

        public async Task<BasicResultDTO<BeneficioDTO>> SolicitarCodigo(int beneficioId)
        {
            return await this.Try(async () =>
            {
                var beneficio = await _beneficioRepository.GetByIdAsync(beneficioId);
                if (beneficio == null)
                    return BasicResultDTO<BeneficioDTO>.Failled("No Existe Beneficio");
                if (beneficio.Aplicado)
                    return BasicResultDTO<BeneficioDTO>.Failled("Beneficio ya Aplicado");
                if (beneficio.FechaPedidoDeAplicacion.HasValue)
                    return BasicResultDTO<BeneficioDTO>.Failled("Ya fue pedido");
                if (!string.IsNullOrEmpty(beneficio.Codigo))
                    return BasicResultDTO<BeneficioDTO>.Failled("Ya posee un codigo.");
                beneficio.FechaPedidoDeAplicacion = DateTime.Now;
                beneficio.Codigo = $"AlDente-{beneficio.Id}";
                await _beneficioRepository.UpdateAsync(beneficio);
                return BasicResultDTO<BeneficioDTO>.Success(MapToDTO(beneficio, await _tipoBeneficioRepository.GetByIdAsync(beneficio.TipoBeneficioId)));
            });
        }

        public async Task<IEnumerable<BeneficioDTO>> Buscar(string codigo)
        {
            return await this.Try(async () =>
            {
                var beneficios = await _beneficioRepository.QueryAsync(x => x.Codigo == codigo);
                List<dynamic> resultados = new List<dynamic>();
                foreach (var item in beneficios)
                {
                    var tipo = await _tipoBeneficioRepository.GetByIdAsync(item.TipoBeneficioId);
                    resultados.Add(new { Beneficio = item, Tipo = tipo });
                }
                return resultados.Select(x => MapToDTO((Beneficio)x.Beneficio, (TipoBeneficio)x.Tipo));
            });
        }

        private BeneficioDTO MapToDTO(Beneficio x, TipoBeneficio tipoBeneficio)
        {

            return new BeneficioDTO
            {
                Aplicado = x.Aplicado,
                ClienteId = x.ClienteId,
                Fecha = x.Fecha,
                FechaAplicado = x.FechaAplicado,
                FechaPedidoDeAplicacion = x.FechaPedidoDeAplicacion,
                Id = x.Id,
                RestauranteId = x.RestauranteId,
                TipoBeneficioId = x.TipoBeneficioId,
                Codigo = x.Codigo,
                Descuento = $"{tipoBeneficio.Descuento}%"
            };
        }
    }
}
