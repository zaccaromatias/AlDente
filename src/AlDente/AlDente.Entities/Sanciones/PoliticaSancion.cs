using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Sanciones
{
   public class PoliticaSancion : Core.EntityBase
    {
        public string Descripcion { get; set; }
        public int TipoSancionId { get; set; }
        public int NumeroMaximo { get; set; }
        public int Periodo { get; set; }
        public int EstadoReservaId { get; set; }
        public int RestauranteId { get; set; }

        private enum Estados
        {
            Asistida = 1,
            NoAsistida = 2,
            Pendiente = 3,
            Cancelada = 4
        }

    }
}
