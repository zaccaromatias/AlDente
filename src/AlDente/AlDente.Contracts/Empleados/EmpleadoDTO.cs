using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Empleados
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        public int DNI { get; set; }
        public string DescripcionPuesto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public EstadosDeUnEmpleado Estado { get; set; }
        public int RestauranteId { get; set; }
    }

    public class EmpleadoDTOValidator : AbstractValidator<EmpleadoDTO>
    {
        public EmpleadoDTOValidator()
        {
            RuleFor(x => x.DNI)
               .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.DNI))
                .WithName(Messages.DNI);
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Email))
                .EmailAddress()
                .WithMessage(Strings.XIsNotAValidEmailAddress(Messages.Email))
                .WithName(Messages.Email);


            RuleFor(x => x.DescripcionPuesto)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description)
                .MaximumLength(200)
                .WithMessage(Strings.XMustBeLessThanYCharacters(Messages.Description, 200)); ;

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

            RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.State))
                .WithName(Messages.State);

        }
    }
}
