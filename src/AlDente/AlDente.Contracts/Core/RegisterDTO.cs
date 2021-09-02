namespace AlDente.Contracts.Core
{
    public class RegisterDTO
    {

        public string Email { get; set; }

        public string RepeatEmail { get; set; }


        public string Nombre { get; set; }


        public string Apellido { get; set; }


        public string Telefono { get; set; }


        public string Password { get; set; }


        public string RepeatPassword { get; set; }

        public int DNI { get; set; }

        public int? UsuarioCreadorId { get; set; }
        public int TipoUsuarioId { get; set; }
    }

}
