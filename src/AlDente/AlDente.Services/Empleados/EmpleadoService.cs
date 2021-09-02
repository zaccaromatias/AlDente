using AlDente.Contracts.Core;
using AlDente.Contracts.Empleados;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Usuarios;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Empleados
{
    public class EmpleadoService : BaseService, IEmpleadoService
    {
        private IUsuarioRepository usuarioRepository;

        public EmpleadoService(IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository)
            : base(unitOfWork)
        {
            usuarioRepository.Attach(this.unitOfWork);
            this.usuarioRepository = usuarioRepository;
            this.CustomValidations.Add("AK_Empleado_Email", "Ya existe un empleado con ese Email");
            this.CustomValidations.Add("AK_Empleado_DNI", "Ya existe un empleado con ese DNI");
        }
        public async Task<IEnumerable<EmpleadoDTO>> GetAll()
        {
            var empleados = await usuarioRepository.QueryAsync(x => x.TipoUsuarioId == (int)TipoDeUsuarios.Empleado);
            return empleados.Select(x => new EmpleadoDTO
            {
                Id = x.Id,
                DNI = x.DNI,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                Password = x.Password,
                FechaCreacion = x.FechaCreacion,
                Estado = (EstadosDeUnUsuario)x.EstadoId


            });
        }
        public async Task Create(EmpleadoDTO empleadoDTO)
        {
            await Try(async () =>
            {
                await usuarioRepository.AddAsync(new Usuario
                {
                    DNI = empleadoDTO.DNI,
                    TipoUsuarioId = (int)TipoDeUsuarios.Empleado,
                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Email = empleadoDTO.Email,
                    Password = empleadoDTO.Password,
                    FechaCreacion = DateTime.Now,
                    EstadoId = (int)empleadoDTO.Estado,
                    Telefono = empleadoDTO.Telefono
                });
            });
        }

        public async Task Delete(int id)
        {
            await usuarioRepository.DeleteAsync(id);
        }

        public async Task Update(EmpleadoDTO empleadoDTO)
        {
            await Try(async () =>
            {
                await usuarioRepository.UpdateAsync(new Usuario
                {
                    Id = empleadoDTO.Id,
                    DNI = empleadoDTO.DNI,
                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Email = empleadoDTO.Email,
                    Password = empleadoDTO.Password,
                    EstadoId = (int)empleadoDTO.Estado,
                    FechaCreacion = empleadoDTO.FechaCreacion,
                    Telefono = empleadoDTO.Telefono,
                    TipoUsuarioId = (int)TipoDeUsuarios.Empleado
                });
            });
        }

    }
}
