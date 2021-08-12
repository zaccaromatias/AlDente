using AlDente.Contracts.Mesas;
using AlDente.Contracts.Turnos;
using System;

namespace AlDente.Contracts.Reservas
{
    public class ReservaDTO
    {
        public int Comensales { get; set; }
        public DateTime Fecha { get; set; }
        public TurnoDTO Turno { get; set; }
        public CombinacionDTO Combinacion { get; set; }
        public int ClienteId { get; set; }
    }
}
