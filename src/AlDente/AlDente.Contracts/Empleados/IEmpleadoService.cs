using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlDente.Contracts.Empleados
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDTO>> GetAll();

        Task Create(EmpleadoDTO empleadoDTO);

        Task Update(EmpleadoDTO empleadoDTO);

        Task Delete(int id);
    }
}
