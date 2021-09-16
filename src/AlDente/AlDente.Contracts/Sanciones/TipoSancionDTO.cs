using AlDente.Contracts.Core;
using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Sanciones
{
    public class TipoSancionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int DiasSuspension { get; set; }

        public string DescripcionCompleta => GetDescripcionCompleta();

        private string GetDescripcionCompleta()
        {
            return (this.Descripcion + " - " + this.DiasSuspension + " Días de Suspensión");
        }
    }
    public class TipoSancionDTOValidator : AbstractValidator<TipoSancionDTO>
    {
        public TipoSancionDTOValidator()
        {
            RuleFor(x => x.Codigo)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Code))
                .WithName(Messages.Code);
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description);

            RuleFor(x => x.DiasSuspension)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.SuspensionDays))
                .WithName(Messages.SuspensionDays);



        }
    }
}
