using AlDente.DataAccess.Core;
using AlDente.Entities.Usuarios;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Usuarios
{
    public class UsuarioRepository : MyBaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IOptions<AppSettings> settings)
            : base(settings)
        {
        }
    }
}
