using AlDente.Contracts.Core;
using AlDente.Contracts.Mesas;
using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Reservas;
using AlDente.Entities.Usuarios;
using MlkPwgen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas
{
    public class ReservaFactory : INeedUnitOfWork, INeedReservaRepository, INeedReservaMesaRepository, INeedClienteRepository, INeedTurnoRepository, INeedMesaRepository
        , IVerifyClientState, IVeryfyTurno, IValidateReserva, ISaveReserva, IReservable
    {
        #region Fields
        IUnitOfWork _unitOfWork;
        IReservaRepository _reservaRepository;
        IReservaMesaRepository _reservaMesaRepository;
        IUsuarioRepository _usuarioRepository;
        ITurnoRepository _turnoRepository;
        IMesaRepository _mesaRepository;
        Reserva _reserva;
        List<MesaDTO> _mesas;
        List<string> _errors = new List<string>();
        #endregion
        #region Constructors
        private ReservaFactory()
        {
        }
        #endregion

        #region Public Methods
        public static INeedUnitOfWork Create()
        {

            return new ReservaFactory();
        }

        public IValidateReserva SetReserva(ReservaDTO reservaDTO)
        {
            _reserva = new Reserva
            {
                CantidadComensales = reservaDTO.Comensales,
                ClienteId = reservaDTO.ClienteId,
                Codigo = PasswordGenerator.Generate(10),
                EmpleadoId = null,
                EstadoReservaId = (int)EstadosDeUnaReserva.Pendiente,
                FechaAsistencia = null,
                FechaCancelacion = null,
                FechaCreacion = DateTime.Now,
                FechaReserva = reservaDTO.Fecha,
                Id = 0,
                MotivoCancelacion = null,
                RestauranteId = this._unitOfWork.RestauranteId,
                TurnoId = reservaDTO.Turno.Id
            };
            _mesas = reservaDTO.Combinacion.Mesas;
            return this;
        }

        public async Task<ISaveReserva> ValidationsAsync()
        {
            return await (await this.ValidateClient()).ValidateTurno();
        }

        public async Task<BasicResultDTO<string>> SaveAsync()
        {
            if (this._errors.Any())
                return BasicResultDTO<string>.Failled(string.Join("<br/>", this._errors));
            return await _unitOfWork.TryWithTransact<BasicResultDTO<string>>(async () =>
            {
                var reservaId = await _reservaRepository.AddAsync(_reserva);
                _reserva.Id = reservaId;
                _reserva.Mesas.ForEach(mesa => mesa.ReservaId = reservaId);
                await _reservaMesaRepository.AddAllAsync(_reserva.Mesas);
                return BasicResultDTO<string>.Success(_reserva.Codigo);
            });
        }

        public INeedTurnoRepository SetClienteRepository(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            return this;
        }

        public INeedReservaMesaRepository SetReservaRepository(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
            return this;
        }
        public INeedClienteRepository SetReservaMesaRepository(IReservaMesaRepository reservaMesaRepository)
        {
            _reservaMesaRepository = reservaMesaRepository;
            return this;
        }

        public INeedMesaRepository SetTurnoRepository(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
            return this;

        }

        public IReservable SetMesaRepository(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
            return this;
        }

        public INeedReservaRepository SetUnitofWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            return this;
        }

        public async Task<IVeryfyTurno> ValidateClient()
        {
            //Valida si el estado del cliente sigue siendo valido para crear una reserva
            Usuario cliente = await _usuarioRepository.GetByIdAsync(_reserva.ClienteId);
            PuedeReservarResult result = cliente.PuedeReservar();
            if (!result.EsValido)
                this._errors.AddRange(result.Motivos);

            //Valida que ya no tenga una reserva
            var reservaPendiente = await _reservaRepository.QueryAsync(x => x.ClienteId == _reserva.ClienteId && x.FechaReserva == _reserva.FechaReserva && _reserva.EstadoReservaId == (int)EstadosDeUnaReserva.Pendiente);
            if (reservaPendiente.Any())
                this._errors.Add("Ya tienes una Reserva para el mismo dia.");
            return this;
        }

        public async Task<ISaveReserva> ValidateTurno()
        {
            //Verifica haya disponibilidad de mesas en la fecha y turno            
            var mesasDisponibles = await _mesaRepository.GetMesasDisponibles(_reserva.FechaReserva, _reserva.TurnoId);
            foreach (var posibleMesa in _mesas)
            {
                var mesaAReservar = mesasDisponibles
                    .Where(x => x.Id == posibleMesa.Id || x.Capacidad == posibleMesa.Capacidad)
                    .Where(x => !_reserva.Mesas.Any(m => m.MesaId == x.Id))
                    .FirstOrDefault();
                if (mesaAReservar != null)
                    _reserva.Mesas.Add(new ReservaMesa
                    {
                        Id = 0,
                        MesaId = mesaAReservar.Id
                    });
                else
                {
                    this._errors.Add("El Conjunto de mesas que eligio ya no se encuentra disponible. Vuelva a Buscar.");
                    break;
                }
            }
            return this;
        }
        #endregion
    }

    #region Interfaces
    public interface INeedUnitOfWork
    {
        INeedReservaRepository SetUnitofWork(IUnitOfWork unitOfWork);
    }

    public interface INeedReservaRepository
    {
        INeedReservaMesaRepository SetReservaRepository(IReservaRepository reservaRepository);
    }

    public interface INeedReservaMesaRepository
    {
        INeedClienteRepository SetReservaMesaRepository(IReservaMesaRepository reservaMesaRepository);
    }

    public interface INeedClienteRepository
    {
        INeedTurnoRepository SetClienteRepository(IUsuarioRepository clienteRepository);
    }

    public interface INeedTurnoRepository
    {
        INeedMesaRepository SetTurnoRepository(ITurnoRepository turnoRepository);
    }


    public interface INeedMesaRepository
    {
        IReservable SetMesaRepository(IMesaRepository mesaRepository);
    }


    public interface IVerifyClientState
    {
        Task<IVeryfyTurno> ValidateClient();
    }

    public interface IVeryfyTurno
    {
        Task<ISaveReserva> ValidateTurno();
    }


    public interface IValidateReserva
    {
        Task<ISaveReserva> ValidationsAsync();
    }

    public interface ISaveReserva
    {
        Task<BasicResultDTO<string>> SaveAsync();
    }

    public interface IReservable
    {
        IValidateReserva SetReserva(ReservaDTO reservaDTO);
    }
    #endregion

}
