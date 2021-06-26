namespace AlDente.Contracts.Clientes
{
    public class ClienteBasicDTO : IMyIdentify
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Identify => Id.ToString();

        public EstadosDeUnCliente Estado { get; set; }
    }
}


