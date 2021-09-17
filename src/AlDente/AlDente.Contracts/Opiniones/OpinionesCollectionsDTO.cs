using System.Collections.Generic;

namespace AlDente.Contracts.Opiniones
{
    public class OpinionesCollectionsDTO
    {
        public int CantidadTotalDeOpiniones { get; set; }

        public int CantidadACargar { get; set; }

        public IEnumerable<OpinionDTO> Opiniones { get; set; }

        public OpinionesCollectionsDTO()
        {
            this.Opiniones = new List<OpinionDTO>();

        }
    }
}
