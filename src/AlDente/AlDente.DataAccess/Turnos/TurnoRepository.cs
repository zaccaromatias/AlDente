using AlDente.DataAccess.Core;
using AlDente.Entities.Turnos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Turnos
{
    public class TurnoRepository : MyBaseRepository<Turno>, ITurnoRepository
    {
        public TurnoRepository(IOptions<AppSettings> settings)
            : base(settings) { }

    }
}
