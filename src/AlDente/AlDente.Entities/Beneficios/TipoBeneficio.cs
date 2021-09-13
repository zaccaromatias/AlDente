using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Beneficios
{
    public class TipoBeneficio : Core.EntityBase
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Descuento { get; set; }
    }
}
