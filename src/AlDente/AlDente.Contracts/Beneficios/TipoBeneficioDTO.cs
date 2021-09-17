using AlDente.Contracts.Core;
using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Beneficios
{
    public class TipoBeneficioDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Descuento { get; set; }

        public string DescripcionCompleta => GetDescripcionCompleta();

        private string GetDescripcionCompleta()
        {
            return (this.Descripcion + " - %" + this.Descuento + " Descuento");
        }

    }
    public class TipoBeneficioDTOValidator : AbstractValidator<TipoBeneficioDTO>
    {
        public TipoBeneficioDTOValidator()
        {
            RuleFor(x => x.Codigo)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Code))
                .WithName(Messages.Code);
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description);

            RuleFor(x => x.Descuento)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Discount))
                .WithName(Messages.Discount);



        }
    }
}
