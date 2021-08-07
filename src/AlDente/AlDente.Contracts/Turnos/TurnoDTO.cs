using AlDente.Globalization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Contracts.Turnos
{
    public class TurnoDTO
    {
        public int Id { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
    }
    public class TurnoDTOValidator : AbstractValidator<TurnoDTO>
    {
        public TurnoDTOValidator()
        {
            RuleFor(x => x.HoraInicio)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("HoraInicio"))
                .WithName("HoraInicio");
            RuleFor(x => x.HoraFin)
              .NotEmpty()
               .WithMessage(Strings.XIsRequired("HoraFin"))
               .WithName("HoraFin");
        }
    }
}