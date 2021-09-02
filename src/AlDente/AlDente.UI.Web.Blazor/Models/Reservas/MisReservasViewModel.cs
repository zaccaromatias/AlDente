using AlDente.Contracts.Core;
using AlDente.Contracts.Reservas;
using AlDente.UI.Web.Blazor.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Reservas
{
    public class MisReservasViewModel
    {
        private IReservaService _reservaService;
        public List<ReservaBasicDTO> Reservas { get; private set; }

        public ReservaACancelarDTO ReservaACancelar { get; private set; }
        public SessionData SessionData { get; private set; }


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

        public string MotivoCancelacion { get; set; }

        private MisReservasViewModel(IReservaService reservaService, SessionData sessionData)
        {
            this.Reservas = new List<ReservaBasicDTO>();
            _reservaService = reservaService;
            SessionData = sessionData;
        }

        public async static Task<MisReservasViewModel> Create(IReservaService reservaService, SessionData sessionData)
        {
            var model = new MisReservasViewModel(reservaService, sessionData);
            await model.LoadReservas();
            return model;

        }

        private async Task LoadReservas()
        {
            var reservas = await _reservaService.GetReservasDeUnCliente(SessionData.User.Id);
            this.Reservas = new List<ReservaBasicDTO>(reservas);
        }
    }
}
