using AlDente.Contracts.Clientes;
using AlDente.DataAccess.Clientes;
using AlDente.DataAccess.Core;
using AlDente.Entities.Clientes;
using AlDente.Globalization;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Clientes
{
    public class ClienteService : BaseService, IClienteService
    {
        private IClienteRepository clienteRepository;

        public ClienteService(IUnitOfWork unitOfWork, IClienteRepository clienteRepository)
            : base(unitOfWork)
        {
            clienteRepository.Attach(this.unitOfWork);
            this.clienteRepository = clienteRepository;
            this.CustomValidations.Add("AK_Cliente_Email", Messages.AK_Cliente_Email);
            this.CustomValidations.Add("AK_Cliente_DNI", Messages.AK_Cliente_DNI);
        }





        public async Task<IEnumerable<ClienteDTO>> GetAll()
        {
            var clientes = await clienteRepository.GetAllAsync();
            return clientes.Select(x => new ClienteDTO
            {
                // Datos del cliente
                Id = x.Id,
                Email = x.Email,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                DNI = x.DNI,
                Password = x.Password,
                Estado = (EstadosDeUnCliente)x.EstadoClienteId,
                Telefono = x.Telefono,
                NombreUsuario = x.NombreUsuario
            });
        }
        public async Task Create(ClienteDTO clienteDTO)
        {
            await Try(async () =>
            {
                await clienteRepository.AddAsync(new Cliente
                {
                    Id = 0,
                    Email = clienteDTO.Email,
                    Nombre = clienteDTO.Nombre,
                    Apellido = clienteDTO.Apellido,
                    DNI = clienteDTO.DNI,
                    Password = clienteDTO.Password,
                    EstadoClienteId = (int)clienteDTO.Estado,
                    Telefono = clienteDTO.Telefono,
                    NombreUsuario = clienteDTO.NombreUsuario
                });
            });
        }

        public async Task Delete(int id)
        {
            await clienteRepository.DeleteAsync(id);
        }

        public async Task Update(ClienteDTO clienteDTO)
        {
            await Try(async () =>
            {
                await clienteRepository.UpdateAsync(new Cliente
                {
                    Id = clienteDTO.Id,
                    Email = clienteDTO.Email,
                    Nombre = clienteDTO.Nombre,
                    Apellido = clienteDTO.Apellido,
                    DNI = clienteDTO.DNI,
                    Password = clienteDTO.Password,
                    EstadoClienteId = (int)clienteDTO.Estado,
                    Telefono = clienteDTO.Telefono,
                    NombreUsuario = clienteDTO.NombreUsuario
                });
            });
        }

    }
}

