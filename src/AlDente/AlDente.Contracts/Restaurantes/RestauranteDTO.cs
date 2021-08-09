using AlDente.Globalization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Contracts.Restaurantes
{
    public class RestauranteDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string UrlMenu { get; set; }
    }

    public class RestauranteDTOValidator : AbstractValidator<RestauranteDTO>
    {
        public RestauranteDTOValidator()
        {
            RuleFor(x => x.Descripcion)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired("Descripcion"))
                .WithName("Descripcion")
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters("Descripcion", 500));
            
            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired("Direccion"))
                .WithName("Direccion")
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters("Direccion", 500));

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Phone))
                .WithName(Messages.Phone)
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Phone, 200)); ;

            RuleFor(x => x.UrlMenu)
                .MaximumLength(2048)
                .WithMessage(Strings.XMustBeLessThanYCharacters("Url Menu", 2048))
                .WithName("Url Menu");


        }
    }
}
