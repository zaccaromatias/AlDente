using AlDente.DataAccess.Core;
using AlDente.Entities.DiasLaborables;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.DiasLaborables
{
    public class DiaLaboralRepository : MyBaseRepository<DiaLaboral>, IDiaLaboralRepository
    {
        public DiaLaboralRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }
    }
}
