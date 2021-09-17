using AlDente.Contracts.Core;
using AlDente.Globalization;
using FluentValidation;
using System;
using AlDente.Contracts.Reservas;

namespace AlDente.Contracts.Beneficios
{
    public class PoliticaBeneficioDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        //public int TipoBeneficioId { get; set; }
        public TipoBeneficioDTO TipoBeneficio { get; set; }
        public int NumeroMinimo { get; set; }
        public int Periodo { get; set; }
        public EstadosDeUnaReserva EstadoReservaId { get; set; }
        public int RestauranteId { get; set; }

        public string EstadoName => GetEstadoName();
        
        public string TipoBeneficioName => GetTipoBeneficioName();
        private string GetEstadoName()
        {
            switch ((EstadosDeUnaReserva)this.EstadoReservaId)
            {
                case EstadosDeUnaReserva.Asistida:
                    return Messages.Assisted;
                case EstadosDeUnaReserva.NoAsistida:
                    return Messages.NotAssisted;
                case EstadosDeUnaReserva.Pendiente:
                    return Messages.Pending;
                case EstadosDeUnaReserva.Cancelada:
                    return Messages.Cancelled;
                default:
                    return "";

            }
        }

      
        private string GetTipoBeneficioName()
        {
            return (this.TipoBeneficio.Descripcion + " - %" + this.TipoBeneficio.Descuento + " Descuento");
        }

    }
    public class PoliticaBeneficioDTOValidator : AbstractValidator<PoliticaBeneficioDTO>
    {
        public PoliticaBeneficioDTOValidator()
        {
            
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description);

            RuleFor(x => x.TipoBeneficio)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.TypeOfBenefit))
               .WithName(Messages.TypeOfBenefit);

            RuleFor(x => x.NumeroMinimo)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.MinimumNumber))
                .WithName(Messages.MinimumNumber);
            RuleFor(x => x.Periodo)
               .NotEmpty()
               .WithMessage(Strings.XIsRequired(Messages.Period))
               .WithName(Messages.Period);

            RuleFor(x => x.EstadoReservaId)
               .NotEmpty()
               .WithMessage(Strings.XIsRequired(Messages.ReservationStatus))
               .WithName(Messages.ReservationStatus);

        }
    }
}