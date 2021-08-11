using AlDente.Globalization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Contracts.Opiniones
{
   public class OpinionDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Calificacion { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
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
