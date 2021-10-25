using AlDente.DataAccess.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Sanciones;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Beneficios;
using AlDente.Entities.Reservas;
using AlDente.Entities.Sanciones;
using AlDente.Services.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas
{
    public class AplicarPoliticasLogic
    {
        IUnitOfWork _unitOfWork;
        IPoliticaBeneficioRepository _politicaBeneficioRepository;
        IPoliticaSancionRepository _politicaSancionRepository;
        IReservaRepository _reservaRepository;
        IBeneficioRepository _beneficioRepository;
        ITipoBeneficioRepository _tipoBeneficioRepository;
        ITipoSancionRepository _tipoSancionRepository;
        IEmailService _emailService;
        ISancionRepository _sancionRepository;

        IUsuarioRepository _usuarioRepository;
        ITurnoRepository _turnoRepository;
        Reserva _reserva;
        private AplicarPoliticasLogic(IUnitOfWork unitOfWork, IPoliticaBeneficioRepository politicaBeneficioRepository, IPoliticaSancionRepository politicaSancionRepository, IReservaRepository reservaRepository, IBeneficioRepository beneficioRepository, ITipoBeneficioRepository tipoBeneficioRepository, IEmailService emailService, IUsuarioRepository usuarioRepository, ITipoSancionRepository tipoSancionRepository, ISancionRepository sancionRepository, ITurnoRepository turnoRepository, Reserva reserva)
        {
            _unitOfWork = unitOfWork;
            _turnoRepository = turnoRepository;
            _politicaBeneficioRepository = politicaBeneficioRepository;
            _beneficioRepository = beneficioRepository;
            _politicaSancionRepository = politicaSancionRepository;
            _tipoBeneficioRepository = tipoBeneficioRepository;
            _reserva = reserva;
            _reservaRepository = reservaRepository;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
            _tipoSancionRepository = tipoSancionRepository;
            _sancionRepository = sancionRepository;
        }

        public static AplicarPoliticasLogic Create(IUnitOfWork unitOfWork, IPoliticaBeneficioRepository politicaBeneficioRepository, IPoliticaSancionRepository politicaSancionRepository, IReservaRepository reservaRepository, IBeneficioRepository beneficioRepository, ITipoBeneficioRepository tipoBeneficioRepository, IEmailService emailService, IUsuarioRepository usuarioRepository, ITipoSancionRepository tipoSancionRepository, ISancionRepository sancionRepository, ITurnoRepository turnoRepository, Reserva reserva)
        {
            return new AplicarPoliticasLogic(unitOfWork, politicaBeneficioRepository, politicaSancionRepository, reservaRepository, beneficioRepository, tipoBeneficioRepository, emailService, usuarioRepository, tipoSancionRepository, sancionRepository, turnoRepository, reserva);
        }

        public async Task<AplicarPoliticasLogic> AsignarBeneficios()
        {
            var politicasBeneficios = await _politicaBeneficioRepository.QueryAsync(x => x.EstadoReservaId == _reserva.EstadoReservaId);


            foreach (var politica in politicasBeneficios.OrderByDescending(x => x.NumeroMinimo))
            {
                DateTime fechaDesde = DateTime.Today.Subtract(TimeSpan.FromDays(politica.Periodo));
                var cantidadDeReservas = await _reservaRepository.CountAsync(x => x.EstadoReservaId == _reserva.EstadoReservaId && x.ClienteId == _reserva.ClienteId && x.FechaReserva >= fechaDesde);
                if (cantidadDeReservas >= politica.NumeroMinimo)
                {
                    await CrearBeneficio(politica);
                    break;
                }
            }
            return this;
        }

        private async Task CrearBeneficio(Entities.Beneficios.PoliticaBeneficio politica)
        {
            var tipoBeneficioPolitica = await _tipoBeneficioRepository.GetByIdAsync(politica.TipoBeneficioId);
            var beneficiosActivos = await _beneficioRepository.GetActivosByCliente(_reserva.ClienteId);
            bool aplicarMejorBeneficio = true;
            if (beneficiosActivos.Count() != 0)
            {

                foreach (var beneficio in beneficiosActivos)
                {
                    var tipoBeneficio = await _tipoBeneficioRepository.GetByIdAsync(beneficio.TipoBeneficioId);
                    if (tipoBeneficio.Descuento >= tipoBeneficioPolitica.Descuento)
                    {
                        aplicarMejorBeneficio = false;
                        break;
                    }
                }
            }
            if (aplicarMejorBeneficio)
                await AddBeneficio(politica);

        }

        private async Task AddBeneficio(PoliticaBeneficio politica)
        {
            await _beneficioRepository.RemoveBeneficiosActivos(_reserva.ClienteId);

            var id = await _beneficioRepository.AddAsync(new Beneficio
            {
                Aplicado = false,
                ClienteId = _reserva.ClienteId,
                Fecha = DateTime.Now,
                FechaAplicado = null,
                FechaPedidoDeAplicacion = null,
                Id = 0,
                RestauranteId = _reserva.RestauranteId,
                TipoBeneficioId = politica.TipoBeneficioId
            });
            await NotificarNuevoBeneficio(id);
        }

        private async Task NotificarNuevoBeneficio(int beneficioId)
        {
            var beneficio = await _beneficioRepository.GetByIdAsync(beneficioId);
            var cliente = await _usuarioRepository.GetByIdAsync(beneficio.ClienteId);
            var tipoBeneficio = await _tipoBeneficioRepository.GetByIdAsync(beneficio.TipoBeneficioId);

            IEmailDataReady emailData = EmailBasicData
                .Create()
                .AddAddress(new FluentEmail.Core.Models.Address(cliente.Email))
                .SetSubject("AlDente Nuevo Beneficio")
                .SetData(new { URL = _unitOfWork.URL, DescuentoDescription = $"{tipoBeneficio.Descuento} %" });

            await _emailService.NuevoBeneficio(emailData);

        }

        public async Task<AplicarPoliticasLogic> AsignarSanciones()
        {
            var politicasDeSanciones = await _politicaSancionRepository.QueryAsync(x => x.EstadoReservaId == _reserva.EstadoReservaId);


            foreach (var politica in politicasDeSanciones.OrderByDescending(x => x.NumeroMaximo))
            {
                DateTime fechaDesde = DateTime.Today.Subtract(TimeSpan.FromDays(politica.Periodo));
                var cantidadDeReservas = await _reservaRepository.CountAsync(x => x.EstadoReservaId == _reserva.EstadoReservaId && x.ClienteId == _reserva.ClienteId && x.FechaReserva >= fechaDesde);
                if (cantidadDeReservas >= politica.NumeroMaximo)
                {
                    await CrearSancion(politica);
                    break;
                }
            }
            return this;
        }

        private async Task CrearSancion(Entities.Sanciones.PoliticaSancion politica)
        {
            var tipoSancionPolitica = await _tipoSancionRepository.GetByIdAsync(politica.TipoSancionId);
            var sancionesActivas = await _sancionRepository.GetActivosByCliente(_reserva.ClienteId);
            bool aplicarSancionMasDura = true;
            if (sancionesActivas.Count() != 0)
            {
                foreach (var sancion in sancionesActivas)
                {
                    var tipoSancion = await _tipoSancionRepository.GetByIdAsync(sancion.TipoSancionId);
                    if (tipoSancion.DiasSuspension >= tipoSancionPolitica.DiasSuspension)
                    {
                        aplicarSancionMasDura = false;
                        break;
                    }
                }
            }
            if (aplicarSancionMasDura)
                await AddSancion(politica);
        }

        private async Task AddSancion(PoliticaSancion politica)
        {
            await _sancionRepository.RemoveSancionesActivas(_reserva.ClienteId);

            var sancion = new Sancion
            {
                ClienteId = _reserva.ClienteId,
                FechaSansion = DateTime.Now,
                Id = 0,
                RestauranteId = _reserva.RestauranteId,
                TipoSancionId = politica.TipoSancionId
            };
            var id = await _sancionRepository.AddAsync(sancion);




            await NotificarNuevaSancion(id, politica);
            await CancelarReservasDentroDelPeriodoSancionado(sancion, politica);

        }

        private async Task CancelarReservasDentroDelPeriodoSancionado(Sancion sancion, PoliticaSancion politica)
        {
            var tipoSancion = await _tipoSancionRepository.GetByIdAsync(politica.TipoSancionId);
            DateTime fechaHasta = sancion.FechaSansion.AddDays(tipoSancion.DiasSuspension);
            var estadoCancelado = (int)Reserva.EstadosDeUnaReserva.Cancelada;
            var reservasACancelar = await _reservaRepository.QueryAsync(x => x.FechaReserva >= sancion.FechaSansion && x.FechaReserva <= fechaHasta && x.EstadoReservaId != estadoCancelado);
            foreach (var reserva in reservasACancelar)
            {
                await CancelarReservaLogic
                    .Create(_unitOfWork, _reservaRepository, _emailService, _usuarioRepository, _turnoRepository, reserva)
                    .Cancelar("Se le aplico una sancion por no asistir");
            }
        }

        private async Task NotificarNuevaSancion(int sancionId, PoliticaSancion politica)
        {
            var sancion = await _sancionRepository.GetByIdAsync(sancionId);
            var cliente = await _usuarioRepository.GetByIdAsync(sancion.ClienteId);
            var tipoSancion = await _tipoSancionRepository.GetByIdAsync(sancion.TipoSancionId);

            IEmailDataReady emailData = EmailBasicData
                .Create()
                .AddAddress(new FluentEmail.Core.Models.Address(cliente.Email))
                .SetSubject("AlDente Nueva Sancion")
                .SetData(new { URL = _unitOfWork.URL, Dias = $"{tipoSancion.DiasSuspension} días", Motivo = $"Mas de {politica.NumeroMaximo} Reservas en estado {_reserva.GetEstadoName()} en los últimos {politica.Periodo} días." });

            await _emailService.NuevaSancion(emailData);

        }
    }
}
