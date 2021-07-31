using AlDente.Globalization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Contracts.Mesas
{
    public class MesaDTO
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }


    }
    public class MesaDTOValidator : AbstractValidator<MesaDTO>
    {
        public MesaDTOValidator()
        {
            RuleFor(x => x.Capacidad)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Capacidad"))
                .WithName("Capacidad");

        }
    }
}
