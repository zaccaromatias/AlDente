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
                    return "Lunes";
                case DiasDeLaSemana.Martes:
                    return "Martes";
                case DiasDeLaSemana.Miercoles:
                    return "Miercoles";
                case DiasDeLaSemana.Jueves:
                    return "Jueves";
                case DiasDeLaSemana.Viernes:
                    return "Viernes";
                case DiasDeLaSemana.Sabado:
                    return "Sabado";
                case DiasDeLaSemana.Domingo:
                    return "Domingo";
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
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Hora Inicio"))
                .WithName("Hora Inicio");

            RuleFor(x => x.HoraFin)
               .NotEmpty()
               .WithMessage(Strings.XIsRequired("Hora Fin"))
               .WithName("Hora Fin");


            RuleFor(x => x.Dia)
              .NotEmpty()
               .WithMessage(Strings.XIsRequired("Dia de la Semana"))
               .WithName("Dia de la Semana");
        }
    }
}
