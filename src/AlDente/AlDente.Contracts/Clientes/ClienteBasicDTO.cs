namespace AlDente.Contracts.Clientes
{
    public class ClienteBasicDTO : IMyIdentify
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Nombre { get; set; }
        public string Identify => Id.ToString();

        public EstadosDeUnCliente Estado { get; set; }
    }

    public class ClienteDTO : IMyIdentify
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public string Identify => Id.ToString();
        public EstadosDeUnCliente Estado { get; set; }
    }
}


