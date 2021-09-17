using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Opiniones
{
    public class OpinionDTO
    {
        public int Id { get; set; }
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

        public string ClienteEmail { get; set; }

        public bool TieneRespuestas { get; set; }
    }

    public class OpinionDTOValidator : AbstractValidator<OpinionDTO>
    {
        public OpinionDTOValidator()
        {
            RuleFor(x => x.Texto)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Opinión"))
                .WithName("Opinión")
                .MaximumLength(200);
            RuleFor(x => x.Calificacion)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Calificacion"))
                .WithName("Calificacion");
        }
    }
}
