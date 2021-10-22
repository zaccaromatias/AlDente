using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Clientes
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAll();

        Task Create(ClienteDTO clienteDTO);

        Task Update(ClienteDTO clienteDTO);

        Task Delete(int id);
        Task<PuedeReservarDTO> ValidarSiClientePuedeReservar(int id);
    }
}
