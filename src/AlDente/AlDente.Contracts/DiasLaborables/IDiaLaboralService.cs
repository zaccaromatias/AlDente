using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.DiasLaborables
{
    public interface IDiaLaboralService
    {
        Task<IEnumerable<DiaLaboralDTO>> GetAll();
        Task Delete(int id);
        Task Create(DiaLaboralDTO mesaDto);
        Task Update(DiaLaboralDTO mesaDto);

        Task<IEnumerable<DiaLaboralDTO>> GetDiasLaborables();
    }
}
