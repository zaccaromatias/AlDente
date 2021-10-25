using AlDente.Contracts.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Reservas
{
    public interface IReservaService
    {
        Task<BasicResultDTO<ReservaBasicDTO>> Create(ReservaDTO reservaDTO);

        Task<IEnumerable<ReservaBasicDTO>> GetReservasDeUnCliente(int clienteId);

        Task<IEnumerable<ReservaBasicDTO>> GetReservaFiltroCodigo(string codigo, DateTime? fecha);
        Task<BasicResultDTO> CancelarReserva(ReservaACancelarDTO reserva);
        Task<BasicResultDTO> Asistida(ReservaBasicDTO reserva);

        Task<BasicResultDTO> NoAsistida(ReservaBasicDTO reserva);
    }
}
