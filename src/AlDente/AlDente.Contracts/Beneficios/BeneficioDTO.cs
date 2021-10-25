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
        public bool PuedeAplicar => !Aplicado && !Expiro;

        public string FechaExpiracion => this.FechaPedidoDeAplicacion?.AddHours(HORAS_MAXIMA).ToString("dd/MM/yyyy HH:mm") ?? string.Empty;

        public string Descuento { get; set; }

        const int HORAS_MAXIMA = 2;
        public bool Expiro => this.FechaPedidoDeAplicacion != null && DateTime.Now > FechaPedidoDeAplicacion.Value.AddHours(HORAS_MAXIMA);
    }
}
