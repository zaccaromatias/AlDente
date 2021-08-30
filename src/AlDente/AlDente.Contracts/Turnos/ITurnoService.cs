
using AlDente.Contracts.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Turnos
{
    public interface ITurnoService
    {
        Task<IEnumerable<TurnoDTO>> GetAll();
        Task Delete(int id);
        Task<BasicResultDTO> Create(TurnoDTO turnoDTO);
        Task<BasicResultDTO> Update(TurnoDTO turnoDTO);

        Task<IEnumerable<TurnoDTO>> GetTurnosDelDia(int diaLaboralId);
    }
}