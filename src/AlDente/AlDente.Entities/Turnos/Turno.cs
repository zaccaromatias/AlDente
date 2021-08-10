using System;

namespace AlDente.Entities.Turnos
{
    public class Turno : Core.EntityBase
    {
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int RestauranteId { get; set; }
    }
}
