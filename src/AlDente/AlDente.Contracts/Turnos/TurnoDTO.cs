using AlDente.Contracts.DiasLaborables;
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

        public int DiaLaboralId { get; set; }

        public DiaLaboralDTO DiaLaboral { get; set; }

        public string Dia => this.DiaLaboral?.DiaName ?? "";

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

            RuleFor(x => x.DiaLaboralId)
               .NotEmpty()
               .WithMessage(Strings.XIsRequired(Messages.Day))
               .WithName(Messages.Day);

            RuleFor(x => x.HoraInicio)
               .NotNull()
               .WithMessage(Strings.XIsRequired(Messages.StartTime))
               .WithName(Messages.StartTime);
            RuleFor(x => x.HoraFin)
              .NotNull()
              .WithMessage(Strings.XIsRequired(Messages.EndTime))
              .WithName(Messages.EndTime);

            When(x => (x.HoraInicio < TimeSpan.FromHours(12) && x.HoraFin < TimeSpan.FromHours(12))
            || (x.HoraInicio >= TimeSpan.FromHours(12) && x.HoraFin >= TimeSpan.FromHours(12)), () =>
             {
                 RuleFor(x => x.HoraFin).GreaterThan(x => x.HoraInicio)
                 .WithMessage(Messages.TheEndTimemMustBeGreaterThanTheStartTime);
             });

        }
    }
}