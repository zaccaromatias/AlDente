using AlDente.Contracts.Core;

namespace AlDente.Contracts.Clientes
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public EstadosDeUnUsuario Estado { get; set; }


        public string NombreCompleto => GetNombreCompleto();
        private string GetNombreCompleto()
        {
            return this.Nombre + " " + this.Apellido;
        }

    }
}


