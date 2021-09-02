using System.Collections.Generic;

namespace AlDente.Cross
{
    public interface IUserSession
    {
        public int? ClienteId { get; set; }
        public int? EmpleadoId { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
