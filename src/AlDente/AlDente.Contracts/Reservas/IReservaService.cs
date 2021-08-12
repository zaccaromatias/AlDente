using System.Threading.Tasks;

namespace AlDente.Contracts.Reservas
{
    public interface IReservaService
    {
        Task<IReservaResult> Create(ReservaDTO reservaDTO);
    }
}
