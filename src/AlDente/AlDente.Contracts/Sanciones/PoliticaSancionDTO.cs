﻿using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.Globalization;
using FluentValidation;
using System;

namespace AlDente.Contracts.Sanciones
{
    public class PoliticaSancionDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoSancionId { get; set; }
        public int NumeroMaximo { get; set; }
        public int Periodo { get; set; }
        public EstadosDeUnaReserva EstadoReservaId { get; set; }
        public int RestauranteId { get; set; }
        public string EstadoName => GetEstadoName();
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
    }
    public class PoliticaSancionDTOValidator : AbstractValidator<PoliticaSancionDTO>
    {
        public PoliticaSancionDTOValidator()
        {

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage(Strings.XIsRequired(Messages.Description))
                .WithName(Messages.Description);

            RuleFor(x => x.NumeroMaximo)
                    .NotEmpty()
                    .WithMessage(Strings.XIsRequired(Messages.MaximumNumber))
                    .WithName(Messages.MaximumNumber);

            RuleFor(x => x.Periodo)
                    .NotEmpty()
                    .WithMessage(Strings.XIsRequired(Messages.Period))
                    .WithName(Messages.Period);


        }
    }
}
