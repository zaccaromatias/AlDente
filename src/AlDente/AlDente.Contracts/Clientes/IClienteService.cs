using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Clientes
{
    public interface IClienteService
    {
        Task<ClienteBasicDTO> Login(LoginDTO loginDTO);
        Task<ClienteBasicDTO> Register(ClienteRegisterDTO dto);

        Task<IEnumerable<ClienteDTO>> GetAll();

    }
}
