using AlDente.Globalization;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AlDente.Contracts.EstadosClientes
{
    public class EstadoClienteDTO
    {
        [Display(Name = nameof(Messages.Id), ResourceType = typeof(Messages))]
        [Key]
        public int Id { get; set; }

        [Display(Name = nameof(Messages.Code), ResourceType = typeof(Messages))]
        public string Codigo { get; set; }

        [Display(Name = nameof(Messages.Description), ResourceType = typeof(Messages))]
        public string Descripcion { get; set; }
    }

    public class EstadoClienteDTOValidator : AbstractValidator<EstadoClienteDTO>
    {
        public EstadoClienteDTOValidator()
        {
            RuleFor(x => x.Codigo)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Code))
                .WithName(Messages.Code);

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description);

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Id))
                .WithName(Messages.Id);
        }
    }
}