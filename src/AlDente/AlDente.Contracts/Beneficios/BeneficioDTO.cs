using System;

namespace AlDente.Contracts.Beneficios
{
    public class BeneficioDTO
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public int TipoBeneficioId { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
        public bool Aplicado { get; set; }

        public DateTime? FechaAplicado { get; set; }
        public DateTime? FechaPedidoDeAplicacion { get; set; }

        public string Codigo { get; set; }

        public bool PuedeSolicitarElCodigo => !Aplicado && string.IsNullOrEmpty(Codigo) && !FechaPedidoDeAplicacion.HasValue;

        public string FechaExpiracion => this.FechaPedidoDeAplicacion?.AddHours(2).ToString("dd/MM/yyyy HH:mm") ?? string.Empty;

        public string Descuento { get; set; }
    }
}
