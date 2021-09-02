using AlDente.Contracts.Clientes;
using System.Threading.Tasks;

namespace AlDente.Contracts.Core
{
    public interface IAuthorizationService
    {
        Task<IAuthorizationEntity> Login(LoginDTO loginDTO);
        Task<IAuthorizationEntity> Register(ClienteRegisterDTO dto);

    }
}
