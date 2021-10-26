using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.UI.Web.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class FiltroReservaViewModel
    {
        private IReservaService _reservaService;

        public string Codigo { get; set; }
        public DateTime? Fecha { get; set; }
        public ReservaACancelarDTO ReservaACancelar { get; private set; }
        public void SetReservaACancelar(ReservaBasicDTO reserva)
        {
            ReservaACancelar = new ReservaACancelarDTO
            {
                Codigo = reserva.Codigo,
                ReservaId = reserva.Id
            };
        }
        public async Task<BasicResultDTO> CancelarReserva()
        {
            return await _reservaService.CancelarReserva(this.ReservaACancelar);
        }
        private SessionData _sessionData;
        public List<ReservaBasicDTO> Reservas { get; private set; }

        public async Task Clean()
        {
            this.Codigo = string.Empty;
            this.Fecha = null;
            this.ReservaACancelar = null;
            this.Reservas = new List<ReservaBasicDTO>();
            await Task.FromResult("");
        }
        public async Task Buscar()
        {
            this.Reservas.Clear();
            var reservas = await _reservaService.GetReservaFiltroCodigo(Codigo, Fecha);
            this.Reservas = new List<ReservaBasicDTO>(reservas);
        }

        private FiltroReservaViewModel(IReservaService reservaService, SessionData sessionData)
        {
            _reservaService = reservaService;
            _sessionData = sessionData;
            this.Reservas = new List<ReservaBasicDTO>();

        }

        public async static Task<FiltroReservaViewModel> Create(IReservaService reservaService, SessionData sessionData)
        {
            var model = new FiltroReservaViewModel(reservaService, sessionData);
            return await Task.FromResult(model);

        }

        public async Task<BasicResultDTO> ReservaAsistida(ReservaBasicDTO reserva)
        {
            return await _reservaService.Asistida(reserva);
        }

        public async Task<BasicResultDTO> ReservaNoAsistida(ReservaBasicDTO reserva)
        {
            return await _reservaService.NoAsistida(reserva);
        }
    }
}
