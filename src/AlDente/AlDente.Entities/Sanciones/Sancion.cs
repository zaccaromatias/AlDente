using System;

namespace AlDente.Entities.Sanciones
{
    public class Sancion : Core.EntityBase
    {
        public DateTime FechaSansion { get; set; }
        public int TipoSancionId { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
    }
}
