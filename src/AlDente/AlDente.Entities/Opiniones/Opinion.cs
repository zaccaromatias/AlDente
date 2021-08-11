using AlDente.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Opiniones
{
    public class Opinion : EntityBase
    {
        public string Texto { get; set; }
        public int Calificacion { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
    }
}
