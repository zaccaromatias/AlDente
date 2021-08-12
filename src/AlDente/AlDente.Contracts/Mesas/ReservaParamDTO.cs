using System;

namespace AlDente.Contracts.Mesas
{
    public class ReservaParamDTO
    {
        public DateTime Fecha { get; set; }
        public int Comensales { get; set; }
        public int TurnoId { get; set; }
    }
}
