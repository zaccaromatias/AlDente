using AlDente.Globalization;
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
        private enum EstadosDeUnaReserva
        {
            Asistida = 1,
            NoAsistida = 2,
            Pendiente = 3,
            Cancelada = 4
        }
        public string GetEstadoName()
        {
            switch ((EstadosDeUnaReserva)this.EstadoReservaId)
            {
                case EstadosDeUnaReserva.Asistida:
                    return Messages.Assisted;
                case EstadosDeUnaReserva.NoAsistida:
                    return Messages.NotAssisted;
                case EstadosDeUnaReserva.Pendiente:
                    return Messages.Pending;
                case EstadosDeUnaReserva.Cancelada:
                    return Messages.Cancelled;
                default:
                    return "";

            }
        }
    }
}
