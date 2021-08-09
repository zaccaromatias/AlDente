using AlDente.DataAccess.Core;
using AlDente.Entities.Empleados;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Empleados
{
    public class EmpleadoRepository : MyBaseRepository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }
    }
}
