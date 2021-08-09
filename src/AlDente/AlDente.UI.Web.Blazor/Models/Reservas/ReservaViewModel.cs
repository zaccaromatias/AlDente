using AlDente.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class ReservaViewModel
    {
        [Display(Name = nameof(Messages.Comensales), ResourceType = typeof(Messages))]
        public int? Comensales { get; set; }

        [Display(Name = nameof(Messages.Date), ResourceType = typeof(Messages))]
        public DateTime? Date { get; set; }

        [Display(Name = nameof(Messages.Turno), ResourceType = typeof(Messages))]
        public TurnoDTO Turno { get; set; }


        public List<TurnoDTO> Turnos { get; set; }

        public List<CombinacionDTO> Combinaciones { get; set; }

        public Guid?[] Combinacion { get; set; }

        public string MensajeDeErrorAlBuscar { get; private set; }
        public string MensajeDeErrorAlReservar { get; private set; }
        public ReservaViewModel()
        {
            this.Turnos = new List<TurnoDTO>()
            {
                new TurnoDTO{Id = 1,Text = "20:00HS" },
                new TurnoDTO{Id = 2,Text = "21:00HS" },
                new TurnoDTO{Id = 2,Text = "22:00HS" }
            };
            this.Combinaciones = new List<CombinacionDTO>();
            this.Combinacion = new Guid?[] { };
        }

        public bool EsValidoParaBuscarCombinaciones => this.Turno != null && this.Comensales > 0 && this.Date.HasValue && this.Date != DateTime.MinValue;
        public bool EsValidoParaReservar => EsValidoParaBuscarCombinaciones && this.Combinacion.Count() == 1 && this.Combinacion.First() != Guid.Empty;
        public async Task CargarCombinaciones()
        {

            if (!EsValidoParaBuscarCombinaciones)
            {
                this.MensajeDeErrorAlBuscar = "Verifique que todos los datos esten ingresados para poder buscar.";
                return;
            }
            this.ClearCombinaciones();
            await Task.Delay(2000);
            this.Combinaciones.Add(new CombinacionDTO
            {
                Mesas = new List<MesasDTO>
                {
                    new MesasDTO{ Id = 1,Capacidad = 2},
                    new MesasDTO{ Id = 2,Capacidad = 2},
                    new MesasDTO{ Id = 2,Capacidad = 2}
                }
            });
            this.Combinaciones.Add(new CombinacionDTO
            {
                Mesas = new List<MesasDTO>
                {
                    new MesasDTO{ Id = 1,Capacidad = 2},
                    new MesasDTO{ Id = 2,Capacidad = 4}
                }
            });

            this.Combinaciones.Add(new CombinacionDTO
            {
                Mesas = new List<MesasDTO>
                {
                    new MesasDTO{ Id = 1,Capacidad = 8}
                }
            });

        }

        public void ClearCombinaciones()
        {
            this.MensajeDeErrorAlBuscar = null;
            this.MensajeDeErrorAlReservar = null;
            this.Combinacion = new Guid?[] { };
            this.Combinaciones.Clear();
        }
        public void Reservar()
        {
            if (!this.EsValidoParaReservar)
            {
                this.MensajeDeErrorAlReservar = "Por favor seleccione una combinacion.";
                return;
            }
            this.MensajeDeErrorAlReservar = "Todo Joya para reservar";
        }
    }



    public class TurnoDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }

    }

    public class CombinacionDTO
    {
        public Guid Key { get; private set; }
        public List<MesasDTO> Mesas { get; set; }
        public CombinacionDTO()
        {
            this.Key = Guid.NewGuid();
            this.Mesas = new List<MesasDTO>();
        }

        public string Description => GetDescripcion();

        public override string ToString()
        {
            return Description;
        }

        private string GetDescripcion()
        {

            var mesasPorCapacidad = this.Mesas.GroupBy(x => x.Capacidad).Select(x => $"{x.Count()} Mesa/s para {x.Key} Peronas");
            return string.Join(" + ", mesasPorCapacidad);



        }
    }

    public class MesasDTO
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }
    }




}
