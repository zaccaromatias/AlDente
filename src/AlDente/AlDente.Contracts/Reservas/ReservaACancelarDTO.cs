using AlDente.Globalization;
using FluentValidation;

namespace AlDente.Contracts.Reservas
{
    public class ReservaACancelarDTO
    {
        public int ReservaId { get; set; }
        public string Codigo { get; set; }
        public string Motivo { get; set; }
    }

    public class ReservaACancelarDTOValidator : AbstractValidator<ReservaACancelarDTO>
    {
        public ReservaACancelarDTOValidator()
        {
            RuleFor(x => x.Motivo)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Reason))
                .WithName(Messages.Reason);

        }
    }
}
