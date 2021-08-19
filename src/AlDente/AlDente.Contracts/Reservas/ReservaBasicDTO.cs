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

        private string GetEstadoName()
        {
            switch ((EstadosDeUnaReserva)this.EstadoId)
            {
                case EstadosDeUnaReserva.Asistida:
                    return "Asistida";
                case EstadosDeUnaReserva.NoAsistida:
                    return "No Asistida";
                case EstadosDeUnaReserva.Pendiente:
                    return "Pendiente";
                case EstadosDeUnaReserva.Cancelada:
                    return "Cancelada";
                default:
                    return "";

            }
        }
    }
}
