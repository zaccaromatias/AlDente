
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Beneficios
{
    public interface ITipoBeneficioService
    {
        Task<IEnumerable<TipoBeneficioDTO>> GetAll();
        Task Delete(int id);
        Task Create(TipoBeneficioDTO tipoBeneficioDto);
        Task Update(TipoBeneficioDTO tipoBeneficioDto);
    }
}
