using System;
using System.Collections.Generic;
using System.Linq;

namespace AlDente.Contracts.Mesas
{
    public class CombinacionDTO
    {
        public Guid Key { get; private set; }
        public List<MesaDTO> Mesas { get; set; }
        public CombinacionDTO()
        {
            this.Key = Guid.NewGuid();
            this.Mesas = new List<MesaDTO>();
        }

        public string Description => GetDescripcion();

        public override string ToString()
        {
            return Description;
        }

        private string GetDescripcion()
        {

            var mesasPorCapacidad = this.Mesas.GroupBy(x => x.Capacidad).Select(x => $"{x.Count()} Mesa/s para {x.Key} Personas");
            return string.Join(" + ", mesasPorCapacidad);



        }
    }
}
