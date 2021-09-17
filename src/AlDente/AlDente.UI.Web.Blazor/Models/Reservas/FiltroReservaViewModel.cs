using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.UI.Web.Blazor.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class FiltroReservaViewModel
    {
        private IReservaService _reservaService;

        public string Codigo { get; set; }
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
        public async Task Buscar()
        {
            this.Reservas.Clear();
            var reservas = await _reservaService.GetReservaFiltroCodigo(Codigo);
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
    }
}
