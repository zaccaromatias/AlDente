namespace AlDente.Contracts.Clientes
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ClienteRegisterDTO
    {

        public string Email { get; set; }

        public string RepeatEmail { get; set; }


        public string Nombre { get; set; }


        public string Apellido { get; set; }


        public string Telefono { get; set; }


        public string Password { get; set; }


        public string RepeatPassword { get; set; }

        public int DNI { get; set; }

        public int? EmpleadoId { get; set; }
    }

}
