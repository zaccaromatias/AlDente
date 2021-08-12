using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Turnos
{
    public class TurnoDTO
    {
        public int Id { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public string Text => $"De {HoraInicio.ToString("hh\\:mm")}Hs a {HoraFin.ToString("hh\\:mm")}Hs";
        public int RestauranteId { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
    public class TurnoDTOValidator : AbstractValidator<TurnoDTO>
    {
        public TurnoDTOValidator()
        {
            RuleFor(x => x.HoraInicio)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Hora Inicio"))
                .WithName("Hora Inicio");
            RuleFor(x => x.HoraFin)
              .NotEmpty()
               .WithMessage(Strings.XIsRequired("Hora Fin"))
               .WithName("Hora Fin");
        }
    }
}