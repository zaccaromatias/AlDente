using AlDente.Contracts.Core;
using AlDente.Contracts.DiasLaborables;
using AlDente.Contracts.Mesas;
using AlDente.Contracts.Reservas;
using AlDente.Contracts.Turnos;
using AlDente.Contracts.Opiniones;
using AlDente.Globalization;
using AlDente.UI.Web.Blazor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Contracts.Clientes;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class ReservaEmpleadoViewModel
    {
        #region Properties
        public ITurnoService TurnoService { get; private set; }
        public IDiaLaboralService DiaLaboralService { get; private set; }
        public IMesaService MesaService { get; private set; }
        public IReservaService ReservaService { get; private set; }

        public IOpinionService OpinionService { get; private set; }
        public IClienteService ClienteService { get; private set; }
        public SessionData SessionData { get; private set; }

        [Display(Name = nameof(Messages.Comensales), ResourceType = typeof(Messages))]
        public int? Comensales { get; set; }

        [Display(Name = nameof(Messages.Date), ResourceType = typeof(Messages))]
        public DateTime? Fecha { get; set; }

        [Display(Name = nameof(Messages.Turno), ResourceType = typeof(Messages))]
        public TurnoDTO Turno { get; set; }

        [Display(Name = nameof(Messages.Client), ResourceType = typeof(Messages))]
        public ClienteDTO Cliente { get; set; }
        public List<ClienteDTO> Clientes { get; set; }
        public List<TurnoDTO> Turnos { get; set; }

        public List<CombinacionDTO> Combinaciones { get; set; }

        public List<DiaLaboralDTO> DiasLaborables { get; private set; }

        public Guid?[] Combinacion { get; set; }

        public string MensajeDeErrorAlBuscar { get; private set; }
        public string MensajeDeErrorAlReservar { get; private set; }

        public string MensajeDeErrorClientes { get; private set; }
        #endregion

        #region Constructors
        private ReservaEmpleadoViewModel(ITurnoService turnoService, IDiaLaboralService diaLaboralService, IMesaService mesaService, IReservaService reservaService, IClienteService clienteService, SessionData sessionData)
        {
            TurnoService = turnoService;
            DiaLaboralService = diaLaboralService;
            MesaService = mesaService;
            Turnos = new List<TurnoDTO>();
            DiasLaborables = new List<DiaLaboralDTO>();
            Combinaciones = new List<CombinacionDTO>();
            Combinacion = new Guid?[] { };
            ReservaService = reservaService;
            SessionData = sessionData;
            ClienteService = clienteService;
        }

        public static async Task<ReservaEmpleadoViewModel> Create(ITurnoService turnoService, IDiaLaboralService diaLaboralService, IMesaService mesaService, IReservaService reservaService, IClienteService clienteService, SessionData sessionData)
        {
            var reservaModel = new ReservaEmpleadoViewModel(turnoService, diaLaboralService, mesaService, reservaService, clienteService, sessionData);
            await reservaModel.LoadDiasLaborables();
            return reservaModel;
        }
        #endregion

        #region Public Methods
        public async Task LoadDiasLaborables()
        {
            this.DiasLaborables.Clear();
            var results = await DiaLaboralService.GetDiasLaborables();
            this.DiasLaborables.AddRange(results);
        }
        public bool EsLaborableElDia(DateTime date)
        {
            var diaDeLaSemana = (int)date.DayOfWeek;
            return date >= DateTime.Today && this.DiasLaborables.Any(x => x.Dia == (DiasDeLaSemana)diaDeLaSemana);
        }

        public async Task<IEnumerable<TurnoDTO>> LoadTurnos(DateTime? dateTime)
        {
            this.ClearCombinaciones();
            this.Turno = null;
            this.Turnos = new List<TurnoDTO>();
            if (dateTime.HasValue)
            {
                var diaDeLaSemana = (int)dateTime.Value.DayOfWeek;
                var diaLaborable = this.DiasLaborables.First(x => x.Dia == (DiasDeLaSemana)diaDeLaSemana);
                var turnos = await TurnoService.GetTurnosDelDia(diaLaborable.Id);
                this.Turnos = new List<TurnoDTO>(turnos);
                if (!this.Turnos.Any())
                    this.MensajeDeErrorAlBuscar = "No hay turnos cargados para este dia";
            }
            return await Task.FromResult(this.Turnos);
        }
        public async Task<IEnumerable<ClienteDTO>> LoadClientes()
        {

            this.Cliente = null;
            this.Clientes = new List<ClienteDTO>();
            var clientes = await ClienteService.GetAll();
            this.Clientes = new List<ClienteDTO>(clientes).OrderBy(x => x.Email).ToList();
            if (!this.Clientes.Any())
                this.MensajeDeErrorClientes = "No hay clientes cargados";
           
            return await Task.FromResult(this.Clientes);
        }
        public bool EsValidoParaBuscarCombinaciones => this.Turno != null && this.Comensales > 0 && this.Comensales <= 8 && this.Fecha.HasValue && this.Fecha != DateTime.MinValue;
        public bool EsValidoParaReservar => EsValidoParaBuscarCombinaciones && this.Combinacion.Count() == 1 && this.Combinacion.First() != Guid.Empty;

        public bool EsClienteValido => this.Cliente != null;
        public async Task CargarCombinaciones()
        {

            if (!EsValidoParaBuscarCombinaciones)
            {
                this.MensajeDeErrorAlBuscar = "Verifique que todos los datos esten ingresados para poder buscar.";
                return;
            }
            this.ClearCombinaciones();

            var results = await MesaService.GetMesasDisponibles(new ReservaParamDTO { Comensales = this.Comensales.Value, Fecha = this.Fecha.Value, TurnoId = this.Turno.Id });
            this.Combinaciones = new List<CombinacionDTO>(results);
            if (!this.Combinaciones.Any())
                this.MensajeDeErrorAlBuscar = "No tenemos mesas disponibles para la fecha y turno indicados.";

        }

        public void ClearCombinaciones()
        {
            this.MensajeDeErrorAlBuscar = null;
            this.MensajeDeErrorAlReservar = null;
            this.Combinacion = new Guid?[] { };
            this.Combinaciones.Clear();
        }
        public async Task<BasicResultDTO<string>> Reservar()
        {
            if (!this.EsValidoParaReservar)
            {
                this.MensajeDeErrorAlReservar = "Por favor seleccione una combinacion.";
                return null;
            }
            if (!this.EsClienteValido) {
                this.MensajeDeErrorClientes = "Seleccione un Cliente";
                return null;
            }

            try
            {
                var result = await this.ReservaService.Create(new ReservaDTO
                {
                    Comensales = this.Comensales.Value,
                    Fecha = this.Fecha.Value,
                    Turno = this.Turno,
                    Combinacion = this.Combinaciones.First(x => x.Key == this.Combinacion.First().Value),
                    ClienteId = this.Cliente.Id,
                    EmpleadoId = SessionData.User.Id

                });
                this.MensajeDeErrorAlReservar = result.AllErrors;
                return result;
            }
            catch (Exception ex)
            {

                this.MensajeDeErrorAlReservar = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
