using AlDente.Entities.Core;
using System;

namespace AlDente.Entities.Usuarios
{
    public class Usuario : EntityBase
    {
        public int DNI { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EstadoId { get; set; }
        public int? UsuarioCreadorId { get; set; }

        public string Telefono { get; set; }
        public string Caption => $"{this.Nombre} {this.Apellido}";

        public enum Estados
        {
            Nuevo = 1,
            Activo = 2,
            Suspendido = 3,
            Inactivo = 4
        }


    }
}
