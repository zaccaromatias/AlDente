using AlDente.Contracts.Core;

namespace AlDente.Contracts.Clientes
{
    public class ClienteBasicDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }

        public EstadosDeUnUsuario Estado { get; set; }
    }
}


