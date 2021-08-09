using AlDente.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Empleados
{
    public class Empleado : EntityBase
    {
        public int DNI { get; set; }
        public string DescripcionPuesto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public int EstadoEmpleadoId { get; set; }
        public int RestauranteId { get; set; }

    }
}
