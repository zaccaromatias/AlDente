namespace AlDente.Entities.Clientes
{
    public class Cliente : Core.EntityBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int DNI { get; set; }
        public int EstadoClienteId { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? EmpleadoId { get; set; }


    }


}
