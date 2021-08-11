using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AlDente.Entities.Core;
using RepoDb.Attributes;

namespace AlDente.Entities.DiasLaborables
{
    public class DiaLaboral : EntityBase
    {
        public int DiaDelaSemana { get; set; }

        [SystemSqlServerTypeMap(SqlDbType.Time)]
        public TimeSpan HoraInicio { get; set; }
        [SystemSqlServerTypeMap(SqlDbType.Time)]
        public TimeSpan HoraFin { get; set; }

        public int RestauranteId { get; set; }

    }
}
