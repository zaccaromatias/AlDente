﻿using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Clientes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Turnos;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas
{
    public class ReservaService : BaseService, IReservaService
    {
        private IReservaRepository _reservaRepository;
        private IReservaMesaRepository _reservaMesaRepository;
        IClienteRepository _clienteRepository;
        ITurnoRepository _turnoRepository;
        IMesaRepository _mesaRepository;
        public ReservaService(IUnitOfWork unitOfWork, IReservaRepository reservaRepository, IClienteRepository clienteRepository, ITurnoRepository turnoRepository, IMesaRepository mesaRepository, IReservaMesaRepository reservaMesaRepository) : base(unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _clienteRepository = clienteRepository;
            _turnoRepository = turnoRepository;
            _mesaRepository = mesaRepository;

            _reservaRepository.Attach(unitOfWork);
            _reservaMesaRepository.Attach(unitOfWork);
            _clienteRepository.Attach(unitOfWork);
            _turnoRepository.Attach(unitOfWork);
            _mesaRepository.Attach(unitOfWork);
        }

        public async Task<IReservaResult> Create(ReservaDTO reservaDTO)
        {
            return await this.Try(async () =>
            {
                ISaveReserva reservaForSave = await ReservaFactory.Create()
                .SetUnitofWork(unitOfWork)
                .SetReservaRepository(_reservaRepository)
                .SetReservaMesaRepository(_reservaMesaRepository)
                .SetClienteRepository(_clienteRepository)
                .SetTurnoRepository(_turnoRepository)
                .SetMesaRepository(_mesaRepository)
                .SetReserva(reservaDTO)
                .ValidationsAsync();

                IReservaResult reserva = await reservaForSave.SaveAsync();
                return reserva;
            });
        }

        public async Task<IEnumerable<ReservaBasicDTO>> GetReservasDeUnCliente(int clienteId)
        {
            var result = await _reservaRepository.QueryAsync(x => x.ClienteId == clienteId);
            return result.OrderBy(x => x.FechaReserva).Select(x => new ReservaBasicDTO
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Comensales = x.CantidadComensales,
                EstadoId = x.EstadoReservaId,
                Fecha = x.FechaReserva,
                FechaDeCreacion = x.FechaCreacion,
                LimiteDeHora = LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR
            });
        }

        const int LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR = 2;
        public async Task<BasicResultDTO> CancelarReserva(ReservaACancelarDTO reservaACancelar)
        {
            return await this.Try(async () =>
           {
               var now = DateTime.Now;
               var reserva = await _reservaRepository.GetByIdAsync(reservaACancelar.ReservaId);
               if (reserva == null)
                   return BasicResultDTO.Failled("La Reserva no existe.");
               if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Asistida)
                   return BasicResultDTO.Failled("La Reserva esta Asistida.");
               if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.NoAsistida)
                   return BasicResultDTO.Failled("La Reserva esta No Asistida.");
               if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Cancelada)
                   return BasicResultDTO.Failled("La Reserva ya fue cancelada.");
               if (now >= reserva.FechaReserva)
                   return BasicResultDTO.Failled("La Reserva va a marcarse como asistida o no asistida ya que ya transcurrio la fecha de la misma.");
               if ((reserva.FechaReserva - now).TotalHours <= LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR)
                   return BasicResultDTO.Failled($"Dentro de las {LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR} horas previas a la reserva no es posible cancelarla.");
               reserva.EstadoReservaId = (int)EstadosDeUnaReserva.Cancelada;
               reserva.MotivoCancelacion = reservaACancelar.Motivo;
               reserva.FechaCancelacion = now;
               await _reservaRepository.UpdateAsync(reserva);
               return BasicResultDTO.Success("La Reserva se cancelo con exito.");
           });
        }
    }
}
