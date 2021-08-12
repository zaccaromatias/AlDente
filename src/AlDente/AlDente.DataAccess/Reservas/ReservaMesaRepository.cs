using AlDente.DataAccess.Core;
using AlDente.Entities.Reservas;
using Microsoft.Extensions.Options;

namespace AlDente.DataAccess.Reservas
{
    public class ReservaMesaRepository : Core.MyBaseRepository<ReservaMesa>, IReservaMesaRepository
    {
        public ReservaMesaRepository(IOptions<AppSettings> settings) : base(settings)
        {
        }
    }
}
