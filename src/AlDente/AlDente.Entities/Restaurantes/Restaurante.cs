using System;
using System.Collections.Generic;
using System.Text;
using AlDente.Entities.Core;

namespace AlDente.Entities.Restaurantes
{
    public class Restaurante : EntityBase
    {
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string UrlMenu { get; set; }
    }
}
