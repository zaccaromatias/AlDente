
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Mesas
{
    public interface IMesaService
    {
        Task<IEnumerable<MesaDTO>> GetAll();
        Task Delete(int id);
        Task Create(MesaDTO mesaDto);
        Task Update(MesaDTO mesaDto);
    }
}
