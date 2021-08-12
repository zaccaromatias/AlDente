using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Clientes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Reservas;
using AlDente.DataAccess.Turnos;
using AlDente.Services.Core;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas
{
    public class ReservaService : BaseService, IReservaService
    {
        private IReservaRepository _reservaRepository;
        private IReservaMesaRepository _reservaMesaRepository;
        IClienteRepository _clienteRepository;
        ITurnoRepository _turnoRepository;
        IMesaRepository _mesaRepository;
        public ReservaService(IUnitOfWork unitOfWork, IReservaRepository reservaRepository, IClienteRepository clienteRepository, ITurnoRepository turnoRepository, IMesaRepository mesaRepository, IReservaMesaRepository reservaMesaRepository) : base(unitOfWork)
        {
            _reservaRepository = reservaRepository;
            _reservaMesaRepository = reservaMesaRepository;
            _clienteRepository = clienteRepository;
            _turnoRepository = turnoRepository;
            _mesaRepository = mesaRepository;

            _reservaRepository.Attach(unitOfWork);
            _reservaMesaRepository.Attach(unitOfWork);
            _clienteRepository.Attach(unitOfWork);
            _turnoRepository.Attach(unitOfWork);
            _mesaRepository.Attach(unitOfWork);
        }

        public async Task<IReservaResult> Create(ReservaDTO reservaDTO)
        {
            return await this.Try(async () =>
            {
                ISaveReserva reservaForSave = await ReservaFactory.Create()
                .SetUnitofWork(unitOfWork)
                .SetReservaRepository(_reservaRepository)
                .SetReservaMesaRepository(_reservaMesaRepository)
                .SetClienteRepository(_clienteRepository)
                .SetTurnoRepository(_turnoRepository)
                .SetMesaRepository(_mesaRepository)
                .SetReserva(reservaDTO)
                .ValidationsAsync();

                IReservaResult reserva = await reservaForSave.SaveAsync();
                return reserva;
            });
        }
    }
}
