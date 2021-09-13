
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Sanciones
{
    public interface IPoliticaSancionService
    {
        Task<IEnumerable<PoliticaSancionDTO>> GetAll();
        Task Delete(int id);
        Task Create(PoliticaSancionDTO politicaSancionDto);
        Task Update(PoliticaSancionDTO politicaSancionDto);
    }
}
