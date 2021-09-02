using System.Collections.Generic;

namespace AlDente.Contracts.Core
{
    public class AuthorizationEntityDTO : IAuthorizationEntity
    {
        public int Id { get; private set; }
        public int TipoUsuarioId { get; private set; }

        public int EstadoId { get; private set; }
        public string Email { get; private set; }

        public List<string> Roles { get; private set; }

        public AuthorizationEntityDTO(int id, int tipoUsuarioId, int estadoId, string email)
        {
            this.Roles = new List<string>();
            Id = id;
            TipoUsuarioId = tipoUsuarioId;
            EstadoId = estadoId;
            Email = email;
            if (this.TipoUsuarioId == (int)TipoDeUsuarios.Cliente)
                this.Roles.Add("Cliente");
            else if (this.TipoUsuarioId == (int)TipoDeUsuarios.Empleado)
            {
                this.Roles.Add("Empleado");
                this.Roles.Add("Cliente");
            }
        }
    }
}
