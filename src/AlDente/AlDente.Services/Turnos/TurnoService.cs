using AlDente.Contracts.Turnos;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Core;
using AlDente.Globalization;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Entities.Turnos;

namespace AlDente.Services.Turnos
{
    public class TurnoService : BaseService, ITurnoService
    {
        private ITurnoRepository turnoRepository;

        public TurnoService(IUnitOfWork unitOfWork, ITurnoRepository turnoRepository)
            : base(unitOfWork)
        {
            turnoRepository.Attach(this.unitOfWork);
            this.turnoRepository = turnoRepository;
        }


        public async Task<IEnumerable<TurnoDTO>> GetAll()
        {
            var turnos = await turnoRepository.GetAllAsync();
            return turnos.Select(x => new TurnoDTO
            {
                Id              = x.Id,
               HoraInicio       = x.HoraInicio,
               HoraFin          = x.HoraFin
            });
        }

        public async Task Create(TurnoDTO turnoDTO)
        {
            await Try(async () =>
            {
                await turnoRepository.AddAsync(new Turno
                {
                    HoraInicio = turnoDTO.HoraInicio,
                    HoraFin    = turnoDTO.HoraFin
                });
            });
        }

        public async Task Delete(int id)
        {
            await turnoRepository.DeleteAsync(id);
        }

        public async Task Update(TurnoDTO turnoDTO)
        {
            await Try(async () =>
            {
                await turnoRepository.UpdateAsync(new Turno 
                {
                    Id          = turnoDTO.Id,
                    HoraInicio  = turnoDTO.HoraInicio,
                    HoraFin     = turnoDTO.HoraFin
                });
            });
        }
    }
}



