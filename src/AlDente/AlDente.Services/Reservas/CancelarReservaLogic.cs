using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Reservas;
using AlDente.Services.Core;
using AlDente.Services.Reservas.Extensions;
using System;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas
{
    public class CancelarReservaLogic : ICancelarReserva
    {
        private Reserva _reserva;
        private IReservaRepository _reservaRepository;
        private IEmailService _emailService;
        private IUnitOfWork unitOfWork;
        IUsuarioRepository _usuarioRepository;
        ITurnoRepository _turnoRepository;

        public const int LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR = 2;
        private CancelarReservaLogic(IUnitOfWork unitOfWork, IReservaRepository reservaRepository, IEmailService emailService, IUsuarioRepository usuarioRepository, ITurnoRepository turnoRepository, Reserva reserva)
        {
            this.unitOfWork = unitOfWork;
            _reserva = reserva;
            _reservaRepository = reservaRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
            _turnoRepository = turnoRepository;
        }

        public static ICancelarReserva Create(IUnitOfWork unitOfWork, IReservaRepository reservaRepository, IEmailService emailService, IUsuarioRepository usuarioRepository, ITurnoRepository turnoRepository, Reserva reserva)
        {
            return new CancelarReservaLogic(unitOfWork, reservaRepository, emailService, usuarioRepository, turnoRepository, reserva);
        }
        public async Task<BasicResultDTO> Cancelar(string motivo)
        {
            var now = DateTime.Now;
            if (_reserva == null)
                return BasicResultDTO.Failled("La Reserva no existe.");
            if ((EstadosDeUnaReserva)_reserva.EstadoReservaId == EstadosDeUnaReserva.Asistida)
                return BasicResultDTO.Failled("La Reserva esta Asistida.");
            if ((EstadosDeUnaReserva)_reserva.EstadoReservaId == EstadosDeUnaReserva.NoAsistida)
                return BasicResultDTO.Failled("La Reserva esta No Asistida.");
            if ((EstadosDeUnaReserva)_reserva.EstadoReservaId == EstadosDeUnaReserva.Cancelada)
                return BasicResultDTO.Failled("La Reserva ya fue cancelada.");
            if (now >= _reserva.FechaReserva)
                return BasicResultDTO.Failled("La Reserva va a marcarse como asistida o no asistida ya que ya transcurrio la fecha de la misma.");
            if ((_reserva.FechaReserva - now).TotalHours <= LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR)
                return BasicResultDTO.Failled($"Dentro de las {LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR} horas previas a la reserva no es posible cancelarla.");
            _reserva.EstadoReservaId = (int)EstadosDeUnaReserva.Cancelada;
            _reserva.MotivoCancelacion = motivo;
            _reserva.FechaCancelacion = now;
            await _reservaRepository.UpdateAsync(_reserva);

            await NotificarReservaCancelada(BasicResultDTO<ReservaBasicDTO>.Success(await _reserva.MapToBasicDTO(_turnoRepository, _usuarioRepository)));
            return BasicResultDTO.Success("La Reserva se cancelo con exito.");
        }

        private async Task NotificarReservaCancelada(BasicResultDTO<ReservaBasicDTO> reserva)
        {
            IEmailDataReady emailData = EmailBasicData.Create()
                .AddAddress(new FluentEmail.Core.Models.Address(reserva.Data.EmailUsuario))
                .SetSubject("AlDente Reserva Cancelada")
                .SetData(new
                {
                    URL = this.unitOfWork.URL,
                    CodigoReserva = reserva.Data.Codigo,
                    ReservaDescription = reserva.Data.Description,
                    Motivo = reserva.Data.MotivoCancelacion
                });

            await _emailService.ReservaCancelada(emailData);
        }
    }

    public interface ICancelarReserva
    {
        Task<BasicResultDTO> Cancelar(string motivo);
    }
}
