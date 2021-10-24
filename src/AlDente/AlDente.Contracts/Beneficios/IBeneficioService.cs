using AlDente.Contracts.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Beneficios
{
    public interface IBeneficioService
    {
        Task<IEnumerable<BeneficioDTO>> GetActiveByCliente(int clienteId);
        Task<BasicResultDTO<BeneficioDTO>> SolicitarCodigo(int beneficioId);
        Task<BasicResultDTO> AplicarBeneficio(int beneficioId);

    }
}
