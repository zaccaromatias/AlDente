using AlDente.Globalization;
using System;

namespace AlDente.Contracts.Reservas
{
    public class ReservaBasicDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Comensales { get; set; }

        public bool ShowCancelButton => this.EstadoId == (int)EstadosDeUnaReserva.Pendiente && DateTime.Now < this.Fecha && (Fecha - DateTime.Now).TotalHours > LimiteDeHora;
        public DateTime FechaDeCreacion { get; set; }

        public int EstadoId { get; set; }

        public string Codigo { get; set; }

        public string EstadoName => GetEstadoName();

        public int LimiteDeHora { get; set; }

        public int OrderByState => GetOrderByState();

        public string Turno { get; set; }

        public string NombreUsuario { get; set; }

        private int GetOrderByState()
        {
            switch ((EstadosDeUnaReserva)this.EstadoId)
            {
                case EstadosDeUnaReserva.Asistida:
                    return 1;
                case EstadosDeUnaReserva.NoAsistida:
                    return 2;
                case EstadosDeUnaReserva.Pendiente:
                    return 0;
                case EstadosDeUnaReserva.Cancelada:
                    return 300;
                default:
                    return 4;

            }
        }

        private string GetEstadoName()
        {
            switch ((EstadosDeUnaReserva)this.EstadoId)
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
