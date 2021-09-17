using AlDente.Entities.Core;
using System;

namespace AlDente.Entities.Opiniones
{
    public class Opinion : EntityBase
    {

        public string Texto { get; set; }
        public int ClienteId { get; set; }
        public int Calificacion { get; set; }
        public int RestauranteId { get; set; }
        public int EstadoId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaAprovacion { get; set; }
        public int? EmpleadoAprovadorId { get; set; }
        public int? OpinionPrincipalId { get; set; }
        public int? RemovidoPor { get; set; }
        public DateTime? RemovidoFecha { get; set; }
        public string RemovidoMotivo { get; set; }

    }
}
