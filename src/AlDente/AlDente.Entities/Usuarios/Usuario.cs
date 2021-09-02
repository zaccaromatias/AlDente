using AlDente.Entities.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        private enum Estados
        {
            Nuevo = 1,
            Activo = 2,
            Suspendido = 3,
            Inactivo = 4
        }

        public PuedeReservarResult PuedeReservar()
        {
            List<string> motivos = new List<string>();
            if (EstadoId == (int)Estados.Inactivo)
                motivos.Add("Te encuentras Inactivo por lo que no puedes reservar.");
            else if (EstadoId == (int)Estados.Suspendido)
                motivos.Add("Te encuentras suspendido por lo que no puedes reservar.");
            if (motivos.Any())
                return PuedeReservarResult.Error(motivos);
            return PuedeReservarResult.Success();
        }
    }

    public class PuedeReservarResult
    {
        public bool EsValido { get; private set; }

        public ReadOnlyCollection<string> Motivos { get; private set; }

        private PuedeReservarResult()
        {
            this.EsValido = true;
            this.Motivos = (new List<string>()).AsReadOnly();
        }
        private PuedeReservarResult(List<string> motivos)
        {
            EsValido = false;
            Motivos = motivos.AsReadOnly();

        }

        public static PuedeReservarResult Success()
        {
            return new PuedeReservarResult();
        }

        public static PuedeReservarResult Error(List<string> motivos)
        {
            return new PuedeReservarResult(motivos);
        }

    }
}
