﻿using AlDente.Contracts.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Reservas
{
    public interface IReservaService
    {
        Task<IReservaResult> Create(ReservaDTO reservaDTO);

        Task<IEnumerable<ReservaBasicDTO>> GetReservasDeUnCliente(int clienteId);

        Task<BasicResultDTO> CancelarReserva(ReservaACancelarDTO reserva);
    }
}