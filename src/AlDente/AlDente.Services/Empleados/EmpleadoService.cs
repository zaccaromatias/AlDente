using AlDente.Contracts.Empleados;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Empleados;
using AlDente.Entities.Empleados;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Empleados
{
    public class EmpleadoService : BaseService, IEmpleadoService
    {
        private IEmpleadoRepository empleadoRepository;

        public EmpleadoService(IUnitOfWork unitOfWork, IEmpleadoRepository empleadoRepository)
            : base(unitOfWork)
        {
            empleadoRepository.Attach(this.unitOfWork);
            this.empleadoRepository = empleadoRepository;
            this.CustomValidations.Add("AK_Empleado_Email", "Ya existe un empleado con ese Email");
            this.CustomValidations.Add("AK_Empleado_DNI", "Ya existe un empleado con ese DNI");
        }
        public async Task<IEnumerable<EmpleadoDTO>> GetAll()
        {
            var empleados = await empleadoRepository.GetAllAsync();
            return empleados.Select(x => new EmpleadoDTO
            {
                Id = x.Id,
                DNI = x.DNI,
                DescripcionPuesto = x.DescripcionPuesto,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                Password = x.Password,
                FechaCreacion = x.FechaCreacion,
                Estado = (EstadosDeUnEmpleado)x.EstadoEmpleadoId,
                RestauranteId = x.RestauranteId

            });
        }
        public async Task Create(EmpleadoDTO empleadoDTO)
        {
            await Try(async () =>
            {
                await empleadoRepository.AddAsync(new Empleado
                {
                    DNI = empleadoDTO.DNI,
                    DescripcionPuesto = empleadoDTO.DescripcionPuesto,
                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Email = empleadoDTO.Email,
                    Password = empleadoDTO.Password,
                    FechaCreacion = DateTime.Now,
                    EstadoEmpleadoId = (int)empleadoDTO.Estado,
                    RestauranteId = this.unitOfWork.RestauranteId
                }); ;
            });
        }

        public async Task Delete(int id)
        {
            await empleadoRepository.DeleteAsync(id);
        }

        public async Task Update(EmpleadoDTO empleadoDTO)
        {
            await Try(async () =>
            {
                await empleadoRepository.UpdateAsync(new Empleado
                {
                    Id = empleadoDTO.Id,
                    DNI = empleadoDTO.DNI,
                    DescripcionPuesto = empleadoDTO.DescripcionPuesto,
                    Nombre = empleadoDTO.Nombre,
                    Apellido = empleadoDTO.Apellido,
                    Email = empleadoDTO.Email,
                    Password = empleadoDTO.Password,
                    EstadoEmpleadoId = (int)empleadoDTO.Estado,
                    FechaCreacion = empleadoDTO.FechaCreacion,
                    RestauranteId = empleadoDTO.RestauranteId
                });
            });
        }

    }
}
