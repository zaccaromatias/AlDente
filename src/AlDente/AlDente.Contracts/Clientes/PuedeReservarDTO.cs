using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlDente.Contracts.Clientes
{
    public class PuedeReservarDTO
    {
        public bool EsValido { get; private set; }

        public ReadOnlyCollection<string> Motivos { get; private set; }

        private PuedeReservarDTO()
        {
            this.EsValido = true;
            this.Motivos = (new List<string>()).AsReadOnly();
        }
        private PuedeReservarDTO(List<string> motivos)
        {
            EsValido = false;
            Motivos = motivos.AsReadOnly();

        }

        private PuedeReservarDTO(string motivo)
        {
            EsValido = false;
            this.Motivos = new List<string> { motivo }.AsReadOnly();
        }

        public static PuedeReservarDTO Success()
        {
            return new PuedeReservarDTO();
        }

        public static PuedeReservarDTO Error(string motivo)
        {
            return new PuedeReservarDTO(motivo);
        }
        public static PuedeReservarDTO Error(List<string> motivos)
        {
            return new PuedeReservarDTO(motivos);
        }
    }
}
