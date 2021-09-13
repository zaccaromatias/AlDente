using System;
using System.Collections.Generic;
using System.Text;

namespace AlDente.Entities.Sanciones
{
    public class TipoSancion : Core.EntityBase 
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int DiasSuspension { get; set; }
    }
}
