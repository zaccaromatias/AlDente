using AlDente.Globalization;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlDente.Contracts.Turnos
{
    public class TurnoDTO
    {
        [Display(Name = nameof(Messages.Email), ResourceType = typeof(Messages))]
        public int Id { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public int DiaLaboralId { get; set; }

        public string Text => $"De {HoraInicio.ToString("hh\\:mm")}Hs a {HoraFin.ToString("hh\\:mm")}Hs";

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
            RuleFor(x => x.HoraFin).LessThanOrEqualTo(x => x.HoraInicio)
               .WithMessage("La hora de inicio debe ser menor a la de fin");
        }
    }
}