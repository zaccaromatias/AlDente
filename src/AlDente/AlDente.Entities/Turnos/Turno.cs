using RepoDb.Attributes;
using System;
using System.Data;

namespace AlDente.Entities.Turnos
{
    public class Turno : Core.EntityBase
    {
        [SystemSqlServerTypeMap(SqlDbType.Time)]
        public TimeSpan HoraInicio { get; set; }
        [SystemSqlServerTypeMap(SqlDbType.Time)]
        public TimeSpan HoraFin { get; set; }

        public int DiaLaboralId { get; set; }

        public string Text => $"De {HoraInicio.ToString("hh\\:mm")}Hs a {HoraFin.ToString("hh\\:mm")}Hs";

        public override string ToString()
        {
            return this.Text;
        }
    }
}
