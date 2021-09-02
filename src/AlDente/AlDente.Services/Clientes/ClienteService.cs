using AlDente.Contracts.Clientes;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Usuarios;
using AlDente.Globalization;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Clientes
{
    public class ClienteService : BaseService, IClienteService
    {
        private IUsuarioRepository usuarioRepository;

        public ClienteService(IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository)
            : base(unitOfWork)
        {
            usuarioRepository.Attach(this.unitOfWork);
            this.usuarioRepository = usuarioRepository;
            this.CustomValidations.Add("AK_Cliente_Email", Messages.AK_Cliente_Email);
            this.CustomValidations.Add("AK_Cliente_DNI", Messages.AK_Cliente_DNI);
        }





        public async Task<IEnumerable<ClienteDTO>> GetAll()
        {
            var clientes = await usuarioRepository.QueryAsync(x => x.TipoUsuarioId == (int)TipoDeUsuarios.Cliente);
            return clientes.Select(x => new ClienteDTO
            {
                // Datos del cliente
                Id = x.Id,
                Email = x.Email,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                DNI = x.DNI,
                Password = x.Password,
                Estado = (EstadosDeUnUsuario)x.EstadoId,
                Telefono = x.Telefono,

            });
        }
        public async Task Create(ClienteDTO clienteDTO)
        {
            await Try(async () =>
            {
                await usuarioRepository.AddAsync(new Usuario
                {
                    Id = 0,
                    Email = clienteDTO.Email,
                    Nombre = clienteDTO.Nombre,
                    Apellido = clienteDTO.Apellido,
                    DNI = clienteDTO.DNI,
                    Password = clienteDTO.Password,
                    EstadoId = (int)clienteDTO.Estado,
                    Telefono = clienteDTO.Telefono,
                    TipoUsuarioId = (int)TipoDeUsuarios.Cliente
                });
            });
        }

        public async Task Delete(int id)
        {
            await usuarioRepository.DeleteAsync(id);
        }

        public async Task Update(ClienteDTO clienteDTO)
        {
            await Try(async () =>
            {
                await usuarioRepository.UpdateAsync(new Usuario
                {
                    Id = clienteDTO.Id,
                    Email = clienteDTO.Email,
                    Nombre = clienteDTO.Nombre,
                    Apellido = clienteDTO.Apellido,
                    DNI = clienteDTO.DNI,
                    Password = clienteDTO.Password,
                    EstadoId = (int)clienteDTO.Estado,
                    Telefono = clienteDTO.Telefono,
                    TipoUsuarioId = (int)TipoDeUsuarios.Cliente
                });
            });
        }

    }
}

