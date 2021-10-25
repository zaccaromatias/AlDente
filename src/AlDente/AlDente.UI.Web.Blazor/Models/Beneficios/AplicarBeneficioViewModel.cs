using AlDente.Contracts.Beneficios;
using AlDente.Contracts.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Beneficios
{
    public class AplicarBeneficioViewModel
    {
        public IBeneficioService BeneficioService { get; private set; }
        public List<BeneficioDTO> Beneficios { get; private set; }

        public string Codigo { get; set; }

        private AplicarBeneficioViewModel(IBeneficioService beneficioService)
        {
            BeneficioService = beneficioService;
            this.Beneficios = new List<BeneficioDTO>();
        }

        public static async Task<AplicarBeneficioViewModel> Create(IBeneficioService beneficioService)
        {
            return await Task.FromResult(new AplicarBeneficioViewModel(beneficioService));
        }

        public void Clear()
        {
            this.Codigo = string.Empty;
            this.Beneficios = new List<BeneficioDTO>();
        }


        public async Task Buscar()
        {
            this.Beneficios.Clear();
            this.Beneficios = new List<BeneficioDTO>(await BeneficioService.Buscar(this.Codigo));
        }

        public async Task<BasicResultDTO> Aplicar(int beneficioId)
        {
            var result = await BeneficioService.AplicarBeneficio(beneficioId);
            return result;
        }
    }
}
