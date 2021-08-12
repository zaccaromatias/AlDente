using AlDente.Entities.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlDente.Entities.Clientes
{
    //public class Cliente : Core.EntityBase
    public class Cliente : EntityBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int DNI { get; set; }
        public int EstadoClienteId { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? EmpleadoId { get; set; }
        public string NombreUsuario { get; set; }

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
            if (EstadoClienteId == (int)Estados.Inactivo)
                motivos.Add("Te encuentras Inactivo por lo que no puedes reservar.");
            else if (EstadoClienteId == (int)Estados.Suspendido)
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
