using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Turnos
{
    public class Turno : Core.EntityBase
    {
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
    }
}
