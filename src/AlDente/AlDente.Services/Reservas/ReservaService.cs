using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Sanciones;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Reservas;
using AlDente.Services.Core;
using AlDente.Services.Reservas.Extensions;
using QRCoder;
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
        IUsuarioRepository _usuarioRepository;
        ITurnoRepository _turnoRepository;
        IMesaRepository _mesaRepository;

        IPoliticaBeneficioRepository _politicaBeneficioRepository;
        IPoliticaSancionRepository _politicaSancionRepository;
        IBeneficioRepository _beneficioRepository;
        ITipoBeneficioRepository _tipoBeneficioRepository;

        ISancionRepository _sancionRepository;
        ITipoSancionRepository _tiposancionRepository;
        private IEmailService emailService;

        public ReservaService(IUnitOfWork unitOfWork, IReservaRepository reservaRepository, IUsuarioRepository usuarioRepository, ITurnoRepository turnoRepository, IMesaRepository mesaRepository, IReservaMesaRepository reservaMesaRepository, IPoliticaBeneficioRepository politicaBeneficioRepository, IPoliticaSancionRepository politicaSancionRepository, IBeneficioRepository beneficioRepository, ITipoBeneficioRepository tipoBeneficioRepository, IEmailService emailService, ISancionRepository sancionRepository, ITipoSancionRepository tiposancionRepository) : base(unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _usuarioRepository = usuarioRepository;
            _turnoRepository = turnoRepository;
            _mesaRepository = mesaRepository;
            _politicaBeneficioRepository = politicaBeneficioRepository;
            _politicaSancionRepository = politicaSancionRepository;
            _beneficioRepository = beneficioRepository;
            _tipoBeneficioRepository = tipoBeneficioRepository;
            _sancionRepository = sancionRepository;
            _tiposancionRepository = tiposancionRepository;


            _reservaRepository.Attach(unitOfWork);
            _reservaMesaRepository.Attach(unitOfWork);
            _usuarioRepository.Attach(unitOfWork);
            _turnoRepository.Attach(unitOfWork);
            _mesaRepository.Attach(unitOfWork);
            _politicaBeneficioRepository.Attach(unitOfWork);
            _politicaSancionRepository.Attach(unitOfWork);
            _beneficioRepository.Attach(unitOfWork);
            _tipoBeneficioRepository.Attach(unitOfWork);
            _sancionRepository.Attach(unitOfWork);
            _tiposancionRepository.Attach(unitOfWork);
            this.emailService = emailService;

        }

        public async Task<BasicResultDTO<ReservaBasicDTO>> Create(ReservaDTO reservaDTO)
        {
            return await this.Try(async () =>
            {
                ISaveReserva reservaForSave = await ReservaFactory.Create()
                .SetUnitofWork(unitOfWork)
                .SetReservaRepository(_reservaRepository)
                .SetReservaMesaRepository(_reservaMesaRepository)
                .SetClienteRepository(_usuarioRepository)
                .SetTurnoRepository(_turnoRepository)
                .SetMesaRepository(_mesaRepository)
                .SetSancionRepository(_sancionRepository)
                .SetReserva(reservaDTO)
                .ValidationsAsync();

                BasicResultDTO<ReservaBasicDTO> reserva = await reservaForSave.SaveAsync();
                if (reserva.IsValid)
                    await NotificarNuevaReserva(reserva);
                return reserva;
            });
        }

        private async Task NotificarNuevaReserva(BasicResultDTO<ReservaBasicDTO> reserva)
        {
            byte[] qrImage = CreateQR(reserva);
            IEmailDataReady emailData = EmailBasicData.Create()
                .AddAddress(new FluentEmail.Core.Models.Address(reserva.Data.EmailUsuario))
                .SetSubject("AlDente Nueva Reserva")
                .SetData(new
                {
                    URL = this.unitOfWork.URL,
                    CodigoReserva = reserva.Data.Codigo,
                    ReservaDescription = reserva.Data.Description,
                    QR = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(qrImage))
                });

            await emailService.NuevaReserva(emailData);
        }

        private byte[] CreateQR(BasicResultDTO<ReservaBasicDTO> reserva)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(reserva.Data.Codigo, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                return qrCode.GetGraphic(20);
            }
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

            await emailService.ReservaCancelada(emailData);
        }

        public async Task<IEnumerable<ReservaBasicDTO>> GetReservasDeUnCliente(int clienteId)
        {
            var result = await _reservaRepository.QueryAsync(x => x.ClienteId == clienteId);
            var tasks = await Task.WhenAll(result.OrderBy(x => x.FechaReserva).Select(async x => await x.MapToBasicDTO(_turnoRepository, _usuarioRepository)));
            return tasks.OrderBy(x => x.OrderByState).ThenBy(x => x.Fecha);
        }

        public async Task<IEnumerable<ReservaBasicDTO>> GetReservaFiltroCodigo(string codigo)
        {
            IEnumerable<Reserva> result;
            if (string.IsNullOrEmpty(codigo))
                result = await _reservaRepository.GetAllAsync();
            else
                result = await _reservaRepository.QueryAsync(x => x.Codigo == codigo);
            var tasks = await Task.WhenAll(result.OrderBy(x => x.FechaReserva).Select(async x => await x.MapToBasicDTO(_turnoRepository, _usuarioRepository)));
            return tasks.OrderBy(x => x.OrderByState).ThenBy(x => x.Fecha);
        }



        public const int LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR = 2;
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

               await NotificarReservaCancelada(BasicResultDTO<ReservaBasicDTO>.Success(await reserva.MapToBasicDTO(_turnoRepository, _usuarioRepository)));

               return BasicResultDTO.Success("La Reserva se cancelo con exito.");
           });
        }

        public async Task<BasicResultDTO> Asistida(ReservaBasicDTO reservaBasicDto)
        {
            return await this.TryWithTransaction(async () =>
            {
                var now = DateTime.Now;
                var reserva = await _reservaRepository.GetByIdAsync(reservaBasicDto.Id);
                if (reserva == null)
                    return BasicResultDTO.Failled("La Reserva no existe.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Asistida)
                    return BasicResultDTO.Failled("La Reserva ya esta Asistida.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.NoAsistida)
                    return BasicResultDTO.Failled("La Reserva ya esta No Asistida.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Cancelada)
                    return BasicResultDTO.Failled("La Reserva ya fue cancelada.");

                reserva.EstadoReservaId = (int)EstadosDeUnaReserva.Asistida;
                reserva.FechaAsistencia = DateTime.Now;
                await _reservaRepository.UpdateAsync(reserva);
                await AplicarPoliticasLogic
                .Create(unitOfWork, _politicaBeneficioRepository, _politicaSancionRepository, _reservaRepository, _beneficioRepository, _tipoBeneficioRepository, emailService, _usuarioRepository, _tiposancionRepository, _sancionRepository, reserva)
                .AsignarBeneficios();
                return BasicResultDTO.Success("La Reserva se marco Asistida.");
            });
        }

        public async Task<BasicResultDTO> NoAsistida(ReservaBasicDTO reservaBasicDto)
        {
            return await this.TryWithTransaction(async () =>
            {
                var now = DateTime.Now;
                var reserva = await _reservaRepository.GetByIdAsync(reservaBasicDto.Id);
                if (reserva == null)
                    return BasicResultDTO.Failled("La Reserva no existe.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Asistida)
                    return BasicResultDTO.Failled("La Reserva ya esta Asistida.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.NoAsistida)
                    return BasicResultDTO.Failled("La Reserva ya esta No Asistida.");
                if ((EstadosDeUnaReserva)reserva.EstadoReservaId == EstadosDeUnaReserva.Cancelada)
                    return BasicResultDTO.Failled("La Reserva ya fue cancelada.");

                reserva.EstadoReservaId = (int)EstadosDeUnaReserva.NoAsistida;
                reserva.FechaAsistencia = DateTime.Now;
                await _reservaRepository.UpdateAsync(reserva);
                await AplicarPoliticasLogic
                .Create(unitOfWork, _politicaBeneficioRepository, _politicaSancionRepository, _reservaRepository, _beneficioRepository, _tipoBeneficioRepository, emailService, _usuarioRepository, _tiposancionRepository, _sancionRepository, reserva)
                .AsignarSanciones();
                return BasicResultDTO.Success("La Reserva se marco como No Asistida.");
            });
        }
    }

}
