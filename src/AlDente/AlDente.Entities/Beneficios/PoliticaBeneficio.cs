namespace AlDente.Entities.Beneficios
{
    public class PoliticaBeneficio : Core.EntityBase
    {
        public string Descripcion { get; set; }
        public int TipoBeneficioId { get; set; }
        public int NumeroMinimo { get; set; }
        public int Periodo { get; set; }
        public int EstadoReservaId { get; set; }
        public int RestauranteId { get; set; }
    }
}
