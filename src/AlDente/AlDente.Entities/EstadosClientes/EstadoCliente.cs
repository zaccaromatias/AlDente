using AlDente.Entities.Core;

namespace AlDente.Entities.EstadosClientes
{
    public class EstadoCliente : EntityBase
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
