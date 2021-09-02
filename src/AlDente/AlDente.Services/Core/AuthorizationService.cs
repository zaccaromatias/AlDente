using AlDente.Contracts.Clientes;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Clientes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Empleados;
using AlDente.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        private IClienteRepository clienteRepository;
        private IEmpleadoRepository empleadoRepository;
        public AuthorizationService(IUnitOfWork unitOfWork, IClienteRepository clienteRepository, IEmpleadoRepository empleadoRepository)
            : base(unitOfWork)
        {
            clienteRepository.Attach(this.unitOfWork);
            empleadoRepository.Attach(this.unitOfWork);
            this.clienteRepository = clienteRepository;
            this.empleadoRepository = empleadoRepository;
            this.CustomValidations.Add("AK_Cliente_Email", Messages.AK_Cliente_Email);
            this.CustomValidations.Add("AK_Cliente_DNI", Messages.AK_Cliente_DNI);
        }
        public async Task<IAuthorizationEntity> Login(LoginDTO loginDTO)
        {
            var result = new AuthorizationEntityDTO();
            var empleado = (await empleadoRepository.QueryAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password)).SingleOrDefault();
            if (empleado != null)
            {
                result.EmpleadoId = empleado.Id;
                result.Email = empleado.Email;
                result.Roles.Add("Empleado");
            }
            var cliente = (await clienteRepository.QueryAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password)).SingleOrDefault();
            if (cliente != null)
            {
                result.ClienteId = cliente.Id;
                result.Email = cliente.Email;
                result.Roles.Add("Cliente");

            }
            if (empleado == null && cliente == null)
                throw new DomainException(Messages.EmailOrPasswordWasNotCorrect);
            return result;
        }

        public async Task<IAuthorizationEntity> Register(ClienteRegisterDTO dto)
        {
            return await this.Try(async () =>
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

        private async Task<IAuthorizationEntity> GetClienteBasicDTO(int id)
        {
            var cliente = await clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                throw new DomainException("Cliente No encontrado");
            return new AuthorizationEntityDTO
            {
                Email = cliente.Email,
                ClienteId = cliente.Id,
                Roles = new System.Collections.Generic.List<string> { "Cliente" }
            };
        }

    }
}
