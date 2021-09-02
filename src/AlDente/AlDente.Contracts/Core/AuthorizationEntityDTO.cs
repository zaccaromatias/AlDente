using System.Collections.Generic;

namespace AlDente.Contracts.Core
{
    public class AuthorizationEntityDTO : IAuthorizationEntity
    {
        public int? ClienteId { get; set; }
        public int? EmpleadoId { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public AuthorizationEntityDTO()
        {
            this.Roles = new List<string>();
        }
    }
}
