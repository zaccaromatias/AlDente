using AlDente.Globalization;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AlDente.UI.Web.Blazor.Models
{
    public class RegisterModel
    {

        [Display(Name = nameof(Messages.Email), ResourceType = typeof(Messages))]
        public string Email { get; set; }

        [Display(Name = nameof(Messages.RepeatEmail), ResourceType = typeof(Messages))]
        public string RepeatEmail { get; set; }

        [Display(Name = nameof(Messages.Name), ResourceType = typeof(Messages))]
        public string Nombre { get; set; }

        [Display(Name = nameof(Messages.LastName), ResourceType = typeof(Messages))]
        public string Apellido { get; set; }

        [Display(Name = nameof(Messages.Phone), ResourceType = typeof(Messages))]
        public string Telefono { get; set; }

        [Display(Name = nameof(Messages.Password), ResourceType = typeof(Messages))]
        public string Password { get; set; }

        [Display(Name = nameof(Messages.RepeatPassword), ResourceType = typeof(Messages))]
        public string RepeatPassword { get; set; }

        [Display(Name = nameof(Messages.DNI), ResourceType = typeof(Messages))]
        public int DNI { get; set; }


    }
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Email))
                .EmailAddress()
                .WithMessage(Strings.XIsNotAValidEmailAddress(Messages.Email))
                .WithName(Messages.Email);

            RuleFor(x => x.RepeatEmail)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.RepeatEmail))
                .EmailAddress()
                .WithMessage(Strings.XIsNotAValidEmailAddress(Messages.RepeatEmail))
                .WithName(Messages.RepeatEmail)
                .Equal(x => x.Email)
                .WithMessage(Strings.TheFieldsXAndYMustBeTheSame(Messages.RepeatEmail, Messages.Email));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Password))
                .WithName(Messages.Password)
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Password, 200));

            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.RepeatPassword))
                .WithName(Messages.RepeatPassword)
                .Equal(x => x.Password)
                .WithMessage(Strings.TheFieldsXAndYMustBeTheSame(Messages.RepeatPassword, Messages.Password))
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.RepeatPassword, 200));

            RuleFor(x => x.DNI)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.DNI))
                .WithName(Messages.DNI);

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Phone))
                .WithName(Messages.Phone)
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.LastName, 200)); ;

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Name))
                .WithName(Messages.Name)
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Name, 500));

            RuleFor(x => x.Apellido)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.LastName))
                .WithName(Messages.LastName)
                .MaximumLength(500)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.LastName, 500));
        }
    }
}

