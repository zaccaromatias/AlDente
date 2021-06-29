using AlDente.Globalization;
using FluentValidation;

namespace AlDente.UI.Web.Blazor.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Email))
                .EmailAddress()
                .WithMessage(Strings.XIsNotAValidEmailAddress(Messages.Email))
                .WithName(Messages.Email);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Password))
                .WithName(Messages.Password);
        }
    }
}
