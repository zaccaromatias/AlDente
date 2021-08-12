using System;
using System.Collections.Generic;

namespace AlDente.Entities.Reservas
{
    public class Reserva : Core.EntityBase
    {
        public string Codigo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaAsistencia { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string MotivoCancelacion { get; set; }
        public int CantidadComensales { get; set; }
        public int EstadoReservaId { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
        public int? EmpleadoId { get; set; }
        public int TurnoId { get; set; }

        public List<ReservaMesa> Mesas { get; set; }

        public Reserva()
        {
            this.Mesas = new List<ReservaMesa>();
        }
    }
}
