
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Turnos
{
    public interface ITurnoService
    {
        Task<IEnumerable<TurnoDTO>> GetAll();
        Task Delete(int id);
        Task Create(TurnoDTO turnoDTO);
        Task Update(TurnoDTO turnoDTO);
    }
}