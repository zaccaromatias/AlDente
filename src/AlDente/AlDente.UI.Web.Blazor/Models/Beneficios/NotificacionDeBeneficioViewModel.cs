using AlDente.Contracts.Beneficios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Beneficios
{
    public class NotificacionDeBeneficioViewModel
    {
        public IBeneficioService BeneficioService { get; private set; }
        public BeneficioDTO Beneficio { get; private set; }
        private NotificacionDeBeneficioViewModel(BeneficioDTO beneficio, IBeneficioService beneficioService)
        {
            BeneficioService = beneficioService;
            Beneficio = beneficio;
        }

        public static async Task<NotificacionDeBeneficioViewModel> Create(BeneficioDTO beneficio, IBeneficioService beneficioService)
        {
            return await Task.FromResult(new NotificacionDeBeneficioViewModel(beneficio, beneficioService));
        }

        public async Task SolicitarCodigo()
        {
            var result = await BeneficioService.SolicitarCodigo(Beneficio.Id);
            if (result.IsValid)
                Beneficio = result.Data;
        }

        public List<BeneficioDTO> Beneficios { get; private set; }


        public async Task Buscar()
        {
            this.Beneficios.Clear();
            var beneficios = await BeneficioService.SolicitarCodigo(Beneficio.Id);
            this.Beneficios = new List<BeneficioDTO>((IEnumerable<BeneficioDTO>)beneficios);
        }
    }
}
