using AlDente.DataAccess.Core;
using AlDente.Entities.Empleados;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Empleados
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
    }
}
