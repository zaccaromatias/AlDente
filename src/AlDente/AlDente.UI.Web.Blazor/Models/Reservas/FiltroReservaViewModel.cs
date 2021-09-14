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
        public List<ReservaBasicDTO> Reservas { get; private set; }
        public async Task Buscar(string codigoFiltro)
        {
            var reservas = await _reservaService.GetReservaFiltroCodigo(codigoFiltro);
            this.Reservas = new List<ReservaBasicDTO>(reservas);
        }
    }
}
