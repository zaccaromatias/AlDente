using AlDente.Contracts.Clientes;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Clientes;
using AlDente.DataAccess.Core;
using AlDente.Globalization;
using AlDente.Services.Core;
using AlDente.Entities.Clientes;
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

        public async Task<ClienteBasicDTO> Login(LoginDTO loginDTO)
        {
            var result = (await clienteRepository.QueryAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password)).SingleOrDefault();
            if (result == null)
                throw new DomainException(Messages.EmailOrPasswordWasNotCorrect);
            return await GetClienteBasicDTO(result.Id);
        }

        public async Task<ClienteBasicDTO> Register(ClienteRegisterDTO dto)
        {
            return await this.Try<ClienteBasicDTO>(async () =>
            {
                var id = await clienteRepository.AddAsync(new Entities.Clientes.Cliente
                {
                    Apellido = dto.Apellido,
                    DNI = dto.DNI,
                    Email = dto.Email,
                    EstadoClienteId = dto.EmpleadoId.HasValue ? (int)EstadosDeUnCliente.Nuevo
                    : (int)EstadosDeUnCliente.Activo,
                    Nombre = dto.Nombre,
                    Password = dto.Password,
                    Telefono = dto.Telefono,
                    EmpleadoId = dto.EmpleadoId

                });
                if (id == 0)
                    throw new DomainException("No se pudo Crear");
                return await GetClienteBasicDTO(id);
            });

        }

        public async Task<ClienteBasicDTO> GetClienteBasicDTO(int id)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new DomainException("Cliente No encontrado");
            return await Task.FromResult(new ClienteBasicDTO
            {
                Email = cliente.Email,
                Id = cliente.Id,
                Estado = (EstadosDeUnCliente)cliente.EstadoClienteId
            });
        }

        public async Task<IEnumerable<ClienteDTO>> GetAll()
        {
            var clientes = await clienteRepository.GetAllAsync();
            return clientes.Select(x => new ClienteDTO
            {
                // Datos del cliente
               Id               = x.Id,
               Email            = x.Email,
               Nombre           = x.Nombre,
               Apellido         = x.Apellido,
               DNI              = x.DNI,
               Password         = x.Password,
               Estado           = (EstadosDeUnCliente)x.EstadoClienteId,
               Telefono         = x.Telefono,
               NombreUsuario    = x.NombreUsuario
            });
        }
        public async Task Create(ClienteDTO clienteDTO)
        {
            await Try(async () =>
            {
                await clienteRepository.AddAsync(new Cliente
                {
                    Id                = clienteDTO.Id,
                    Email             = clienteDTO.Email,
                    Nombre            = clienteDTO.Nombre,
                    Apellido          = clienteDTO.Apellido,
                    DNI               = clienteDTO.DNI,
                    Password          = clienteDTO.Password,
                    EstadoClienteId   = (int)(EstadosDeUnCliente)clienteDTO.Estado,
                    Telefono          = clienteDTO.Telefono,
                    NombreUsuario     = clienteDTO.NombreUsuario
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
                    Id              = clienteDTO.Id,
                    Email           = clienteDTO.Email,
                    Nombre          = clienteDTO.Nombre,
                    Apellido        = clienteDTO.Apellido,
                    DNI             = clienteDTO.DNI,
                    Telefono        = clienteDTO.Telefono,
                    NombreUsuario   = clienteDTO.NombreUsuario
                });
            });
        }

    }
}

