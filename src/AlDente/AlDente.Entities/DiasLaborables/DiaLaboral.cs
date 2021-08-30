using AlDente.Entities.Core;
using RepoDb.Attributes;
using System;
using System.Data;

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

        public string Horario => $"De {HoraInicio.ToString("hh\\:mm")}Hs a {HoraFin.ToString("hh\\:mm")}Hs";

        public string DiaName => GetDiaName();

        private string GetDiaName()
        {
            switch ((DiasDeLaSemana)DiaDelaSemana)
            {
                case DiasDeLaSemana.Lunes:
                    return "Lunes";
                case DiasDeLaSemana.Martes:
                    return "Martes";
                case DiasDeLaSemana.Miercoles:
                    return "Miercoles";
                case DiasDeLaSemana.Jueves:
                    return "Jueves";
                case DiasDeLaSemana.Viernes:
                    return "Viernes";
                case DiasDeLaSemana.Sabado:
                    return "Sabado";
                case DiasDeLaSemana.Domingo:
                    return "Domingo";
                default:
                    return "";
            }
        }
    }

    public enum DiasDeLaSemana : int
    {
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6,
        Domingo = 7
    }
}
