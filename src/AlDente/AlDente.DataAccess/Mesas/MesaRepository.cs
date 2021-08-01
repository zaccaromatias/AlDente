using AlDente.DataAccess.Core;
using AlDente.Entities.Mesas;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Mesas
{
    public class MesaRepository : MyBaseRepository<Mesa>, IMesaRepository 
    {
        public MesaRepository(IOptions<AppSettings> settings)
            : base(settings) { }

    }
}
