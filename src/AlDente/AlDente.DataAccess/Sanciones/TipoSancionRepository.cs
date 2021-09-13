using AlDente.DataAccess.Core;
using AlDente.Entities.Sanciones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Sanciones
{
    public class TipoSancionRepository : MyBaseRepository<TipoSancion>, ITipoSancionRepository
    {
        public TipoSancionRepository(IOptions<AppSettings> settings)
            : base(settings) { }

    }
}
