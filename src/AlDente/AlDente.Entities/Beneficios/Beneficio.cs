using System;

namespace AlDente.Entities.Beneficios
{
    public class Beneficio : Core.EntityBase
    {
        public DateTime Fecha { get; set; }
        public int TipoBeneficioId { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
        public bool Aplicado { get; set; }

        public DateTime? FechaAplicado { get; set; }
        public DateTime? FechaPedidoDeAplicacion { get; set; }
    }
}
