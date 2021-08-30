using AlDente.Globalization;
using FluentValidation;

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
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description)
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Description, 500));

            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Address))
                .WithName(Messages.Address)
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Address, 500));

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Phone))
                .WithName(Messages.Phone)
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Phone, 200)); ;

            RuleFor(x => x.UrlMenu)
                .MaximumLength(2048)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Url, 2048))
                .WithName(Messages.Url);


        }
    }
}
