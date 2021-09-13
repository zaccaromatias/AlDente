using AlDente.DataAccess.Core;
using AlDente.Entities.Sanciones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Sanciones
{
    public class PoliticaSancionRepository : MyBaseRepository<PoliticaSancion>, IPoliticaSancionRepository
    {
        public PoliticaSancionRepository(IOptions<AppSettings> settings)
            : base(settings) { }

    }
}
