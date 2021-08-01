using AlDente.Globalization;
using FluentValidation;

namespace AlDente.UI.Web.Blazor.Models
{   // Modelo que voy a usar para validar LOGIN
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
    // Modelo que voy a usar para validar CLIENTE
    public class ClienteViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
    }
    // Validador
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            // Regla del objeto X, si el Email de X esta vacio o es invalido
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Email))
                .EmailAddress()
                .WithMessage(Strings.XIsNotAValidEmailAddress(Messages.Email))
                .WithName(Messages.Email);
            // Si el objeto x tiene el password incorrecto
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Password))
                .WithName(Messages.Password);
        }
    }

    public class ClienteViewModelValidator : AbstractValidator<ClienteViewModel>
    {
        public ClienteViewModelValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("Debe ingresar nombre de cliente");
            RuleFor(x => x.Apellido)
                .NotEmpty()
                .WithMessage("Debe ingresar apellido de cliente");
            RuleFor(x => x.DNI)
                .NotEmpty()
                .WithMessage("Debe ingresar DNI");
            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage("Debe ingresar telefono");
            RuleFor(x => x.NombreUsuario)
                .NotEmpty()
                .WithMessage("Debe ingresar nombre de usuario");
        }
    }

}
