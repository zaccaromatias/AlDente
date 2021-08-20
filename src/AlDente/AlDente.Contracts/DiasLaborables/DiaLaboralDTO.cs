using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.DiasLaborables
{
    public class DiaLaboralDTO
    {

        public int Id { get; set; }
        public DiasDeLaSemana Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int RestauranteId { get; set; }

        public string DiaName => GetDiaName();

        private string GetDiaName()
        {
            switch (Dia)
            {
                case DiasDeLaSemana.Lunes:
                    return Messages.Monday;
                case DiasDeLaSemana.Martes:
                    return Messages.Tuesday;
                case DiasDeLaSemana.Miercoles:
                    return Messages.Wednesday;
                case DiasDeLaSemana.Jueves:
                    return Messages.Thursday;
                case DiasDeLaSemana.Viernes:
                    return Messages.Friday;
                case DiasDeLaSemana.Sabado:
                    return Messages.Saturday;
                case DiasDeLaSemana.Domingo:
                    return Messages.Sunday;
                default:
                    return "";
            }
        }

        public override string ToString()
        {
            return this.DiaName;
        }
    }
    public class DiaLaboralDTOValidator : AbstractValidator<DiaLaboralDTO>
    {
        public DiaLaboralDTOValidator()
        {
            RuleFor(x => x.HoraInicio)
               .NotNull()
                .WithMessage(Strings.XIsRequired(Messages.StartTime))
                .WithName(Messages.StartTime);

            RuleFor(x => x.HoraFin)
               .NotNull()
               .WithMessage(Strings.XIsRequired(Messages.EndTime))
               .WithName(Messages.EndTime);

            RuleFor(x => x.Dia)
              .NotEmpty()
               .WithMessage(Strings.XIsRequired(Messages.Day))
               .WithName(Messages.Day);

            When(x => (x.HoraInicio < TimeSpan.FromHours(12) && x.HoraFin < TimeSpan.FromHours(12))
                || (x.HoraInicio >= TimeSpan.FromHours(12) && x.HoraFin >= TimeSpan.FromHours(12)), () =>
                {
                    RuleFor(x => x.HoraFin).GreaterThan(x => x.HoraInicio)
                .WithMessage(Messages.TheEndTimemMustBeGreaterThanTheStartTime);
                });

        }
    }
}
