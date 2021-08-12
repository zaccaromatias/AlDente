using AlDente.DataAccess.Core;
using AlDente.Entities.Reservas;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Reservas
{
    public class ReservaRepository : Core.MyBaseRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(IOptions<AppSettings> settings) : base(settings)
        {
        }
    }
}
