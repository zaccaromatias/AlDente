using AlDente.Globalization;
using FluentValidation;

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
                .WithMessage(Strings.XIsRequired(Messages.Capacity))
                .WithName(Messages.Capacity);

        }
    }
}
