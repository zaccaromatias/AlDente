
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Beneficios
{
    public interface IPoliticaBeneficioService
    {
        Task<IEnumerable<PoliticaBeneficioDTO>> GetAll();
        Task Delete(int id);
        Task Create(PoliticaBeneficioDTO politicaBeneficioDto);
        Task Update(PoliticaBeneficioDTO politicaBeneficioDto);
    }
}