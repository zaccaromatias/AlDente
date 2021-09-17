using System;

namespace AlDente.Contracts.Opiniones
{
    public class MasOpinionesParameterDTO
    {
        public DateTime FechaDeLaUltimaOpinionCargada { get; set; }

        public int CantidadTotalDeOpiniones { get; set; }

        public int ClienteId { get; set; }
        public int OpinionPrincipalId { get; set; }

        public bool SoloMisOpiniones { get; set; }
    }
}
