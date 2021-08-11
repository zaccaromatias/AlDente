using AlDente.DataAccess.Core;
using AlDente.Entities.Opiniones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.DataAccess.Opiniones
{
    public class OpinionRepository : MyBaseRepository<Opinion>, IOpinionRepository
    {
        public OpinionRepository(IOptions<AppSettings> settings)
            : base(settings) { }

    }
}
