using AlDente.Contracts.Mesas;
using AlDente.Contracts.Reservas;
using AlDente.Contracts.Restaurantes;
using AlDente.Contracts.Turnos;
using AlDente.Globalization;
using AlDente.UI.Web.Blazor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class ReservaViewModel
    {
        public IRestauranteService RestauranteService { get; private set; }
        public IMesaService MesaService { get; private set; }
        public IReservaService ReservaService { get; private set; }

        [Display(Name = nameof(Messages.Comensales), ResourceType = typeof(Messages))]
        public int? Comensales { get; set; }

        [Display(Name = nameof(Messages.Date), ResourceType = typeof(Messages))]
        public DateTime? Fecha { get; set; }

        [Display(Name = nameof(Messages.Turno), ResourceType = typeof(Messages))]
        public TurnoDTO Turno { get; set; }


        public List<TurnoDTO> Turnos { get; set; }

        public List<CombinacionDTO> Combinaciones { get; set; }

        public List<DiaLaborableDTO> DiasLaborables { get; private set; }

        public Guid?[] Combinacion { get; set; }

        public string MensajeDeErrorAlBuscar { get; private set; }
        public string MensajeDeErrorAlReservar { get; private set; }
        private ReservaViewModel(IRestauranteService restauranteService, IMesaService mesaService, IReservaService reservaService)
        {
            RestauranteService = restauranteService;
            MesaService = mesaService;
            this.Turnos = new List<TurnoDTO>();
            this.DiasLaborables = new List<DiaLaborableDTO>();
            this.Combinaciones = new List<CombinacionDTO>();
            this.Combinacion = new Guid?[] { };
            ReservaService = reservaService;
        }

        public static async Task<ReservaViewModel> Create(IRestauranteService restauranteService, IMesaService mesaService, IReservaService reservaService)
        {
            var reservaModel = new ReservaViewModel(restauranteService, mesaService, reservaService);
            await reservaModel.LoadDiasLaborables();
            return reservaModel;
        }
        public async Task LoadDiasLaborables()
        {
            this.DiasLaborables.Clear();
            var results = await RestauranteService.GetDiasLaborables();
            this.DiasLaborables.AddRange(results);
        }
        public bool EsLaborableElDia(int diaDeLaSemana)
        {
            return this.DiasLaborables.Any(x => x.DiaDeLaSemana == diaDeLaSemana);
        }

        public async Task<IEnumerable<TurnoDTO>> LoadTurnos(DateTime? dateTime)
        {
            this.ClearCombinaciones();
            this.Turno = null;
            this.Turnos = new List<TurnoDTO>();
            if (dateTime.HasValue)
            {
                var diaDeLaSemana = (int)dateTime.Value.DayOfWeek;
                var diaLaborable = this.DiasLaborables.FirstOrDefault(x => x.DiaDeLaSemana == diaDeLaSemana);
                this.Turnos.AddRange(diaLaborable?.Turnos);
            }
            return await Task.FromResult(this.Turnos);
        }

        public bool EsValidoParaBuscarCombinaciones => this.Turno != null && this.Comensales > 0 && this.Comensales <= 8 && this.Fecha.HasValue && this.Fecha != DateTime.MinValue;
        public bool EsValidoParaReservar => EsValidoParaBuscarCombinaciones && this.Combinacion.Count() == 1 && this.Combinacion.First() != Guid.Empty;
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
        public async Task<IReservaResult> Reservar()
        {
            if (!this.EsValidoParaReservar)
            {
                this.MensajeDeErrorAlReservar = "Por favor seleccione una combinacion.";
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
                    ClienteId = UserSession.Data.User.Id

                });
                this.MensajeDeErrorAlReservar = result.Error;
                return result;
            }
            catch (Exception ex)
            {

                this.MensajeDeErrorAlReservar = ex.Message;
                return null;
            }
        }
    }
}
