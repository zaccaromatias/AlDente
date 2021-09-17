using AlDente.Contracts.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Reservas
{
    public interface IReservaService
    {
        Task<BasicResultDTO<string>> Create(ReservaDTO reservaDTO);

        Task<IEnumerable<ReservaBasicDTO>> GetReservasDeUnCliente(int clienteId);

        Task<IEnumerable<ReservaBasicDTO>> GetReservaFiltroCodigo(string codigo);
        Task<BasicResultDTO> CancelarReserva(ReservaACancelarDTO reserva);
    }
}
