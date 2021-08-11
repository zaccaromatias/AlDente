using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AlDente.Globalization;
using FluentValidation;

namespace AlDente.Contracts.DiasLaborables
{
    public class DiaLaboralDTO
    {
       
        public int Id { get; set; }
        public DiasDeLaSemana Dia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int RestauranteId { get; set; }
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
