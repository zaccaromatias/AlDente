using System.Collections.Generic;

namespace AlDente.Cross
{
    public interface IUserSession
    {
        public int Id { get; }
        public int TipoUsuarioId { get; }
        public string Email { get; }

        public int EstadoId { get; }

        public List<string> Roles { get; }
    }
}
