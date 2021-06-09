using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.EstadosClientes
{
    public interface IEstadoClienteService
    {
        Task<IEnumerable<EstadoClienteDTO>> GetAll();

        Task Delete(int id);

        Task Create(EstadoClienteDTO estadoClienteDto);
        Task Update(EstadoClienteDTO estadoClienteDto);

        Task<EstadoClienteDTO> GetById(int id);
    }
}
