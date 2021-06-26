using System.ComponentModel.DataAnnotations;

namespace AlDente.UI.Web.Blazor.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string RepeatEmail { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string RepeatPassword { get; set; }
        [Required]
        public int DNI { get; set; }
    }
}

