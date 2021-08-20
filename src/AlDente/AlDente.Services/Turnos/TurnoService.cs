using AlDente.Contracts.Core;
using AlDente.Contracts.DiasLaborables;
using AlDente.Contracts.Turnos;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.DiasLaborables;
using AlDente.DataAccess.Turnos;
using AlDente.Entities.Turnos;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Turnos
{
    public class TurnoService : BaseService, ITurnoService
    {
        private ITurnoRepository _turnoRepository;
        private IDiaLaboralRepository _diaLaboralRepository;

        public TurnoService(IUnitOfWork unitOfWork, ITurnoRepository turnoRepository, IDiaLaboralRepository diaLaboralRepository)
            : base(unitOfWork)
        {
            turnoRepository.Attach(this.unitOfWork);
            diaLaboralRepository.Attach(this.unitOfWork);
            this._turnoRepository = turnoRepository;
            this._diaLaboralRepository = diaLaboralRepository;
        }


        public async Task<IEnumerable<TurnoDTO>> GetAll()
        {
            var turnos = await _turnoRepository.GetAllAsync();
            var tasks = await Task.WhenAll(turnos.Select(async x => await MapToDTOAsync(x)));
            return tasks.OrderBy(x => x.DiaLaboral.Dia).ThenBy(x => x.HoraInicio);
        }


        private async Task<TurnoDTO> MapToDTOAsync(Turno x)
        {
            var dto = new TurnoDTO
            {
                Id = x.Id,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                DiaLaboralId = x.DiaLaboralId

            };
            dto.DiaLaboral = await GetDiaDeLaSemana(x.DiaLaboralId);
            return dto;

        }
        private async Task<DiaLaboralDTO> GetDiaDeLaSemana(int diaLaboralId)
        {
            var diaLaboral = await _diaLaboralRepository.GetByIdAsync(diaLaboralId);
            return new DiaLaboralDTO
            {
                Dia = (DiasDeLaSemana)diaLaboral.DiaDelaSemana,
                HoraFin = diaLaboral.HoraFin,
                HoraInicio = diaLaboral.HoraInicio,
                Id = diaLaboral.Id,
                RestauranteId = diaLaboral.RestauranteId
            };

        }

        public async Task<BasicResultDTO> Create(TurnoDTO turnoDTO)
        {
            return await Try(async () =>
            {
                var validation = await ValidateTurno(turnoDTO);
                if (!validation.IsValid)
                    return validation;
                await _turnoRepository.AddAsync(new Turno
                {
                    HoraInicio = turnoDTO.HoraInicio,
                    HoraFin = turnoDTO.HoraFin,
                    DiaLaboralId = turnoDTO.DiaLaboralId

                });
                return BasicResultDTO.Success();
            });
        }

        private async Task<BasicResultDTO> ValidateTurno(TurnoDTO turnoDTO)
        {
            var diaLaboral = await _diaLaboralRepository.GetByIdAsync(turnoDTO.DiaLaboralId);
            if (diaLaboral == null)
                return BasicResultDTO.Failled("El Dia Laboral Seleccionado no existe");

            if (!IsTimeInRange(turnoDTO.HoraInicio, diaLaboral.HoraInicio, diaLaboral.HoraFin)
                || !IsTimeInRange(turnoDTO.HoraFin, diaLaboral.HoraInicio, diaLaboral.HoraFin))
            {
                return BasicResultDTO.Failled($"Los turnos deben estar dentro del horario del dia laboral: {diaLaboral.Horario}");
            }
            var allTurnos = await _turnoRepository.QueryAsync(x => x.DiaLaboralId == turnoDTO.DiaLaboralId && x.Id != turnoDTO.Id);
            var turnosSolapados = allTurnos
                .Where(x => (IsTimeInRange(turnoDTO.HoraInicio, x.HoraInicio, x.HoraFin) && !IsInTheLimite(turnoDTO.HoraInicio, x.HoraInicio, x.HoraFin)) || (IsTimeInRange(turnoDTO.HoraFin, x.HoraInicio, x.HoraFin) && !IsInTheLimite(turnoDTO.HoraFin, x.HoraInicio, x.HoraFin)))
                .Union(allTurnos.Where(x => turnoDTO.HoraInicio == x.HoraInicio && turnoDTO.HoraFin == x.HoraFin))
                .Union(allTurnos.Where(x => (IsTimeInRange(x.HoraInicio, turnoDTO.HoraInicio, turnoDTO.HoraFin) && !IsInTheLimite(x.HoraInicio, turnoDTO.HoraInicio, turnoDTO.HoraFin)) || (IsTimeInRange(x.HoraFin, turnoDTO.HoraInicio, turnoDTO.HoraFin) && !IsInTheLimite(x.HoraFin, turnoDTO.HoraInicio, turnoDTO.HoraFin))));
            if (turnosSolapados.Any())
                return BasicResultDTO.Failled($"Ya existe un turno que comprende esos horarios.");
            return BasicResultDTO.Success();

        }

        public async Task Delete(int id)
        {
            await _turnoRepository.DeleteAsync(id);
        }

        public async Task<BasicResultDTO> Update(TurnoDTO turnoDTO)
        {
            return await Try(async () =>
            {
                var validation = await ValidateTurno(turnoDTO);
                if (!validation.IsValid)
                    return validation;
                await _turnoRepository.UpdateAsync(new Turno
                {
                    Id = turnoDTO.Id,
                    HoraInicio = turnoDTO.HoraInicio,
                    HoraFin = turnoDTO.HoraFin,
                    DiaLaboralId = turnoDTO.DiaLaboralId
                });
                return BasicResultDTO.Success();
            });
        }

        public async Task<IEnumerable<TurnoDTO>> GetTurnosDelDia(int diaLaboralId)
        {
            var results = await _turnoRepository.QueryAsync(x => x.DiaLaboralId == diaLaboralId);
            var tasks = await Task.WhenAll(results.Select(async x => await MapToDTOAsync(x)));
            return tasks.OrderBy(x => x.HoraInicio);
        }

        private static bool IsInTheLimite(TimeSpan time, TimeSpan horaInicio, TimeSpan horaFin)
        {
            return time == horaInicio || time == horaFin;
        }
        private static bool IsTimeInRange(TimeSpan time, TimeSpan horaInicio, TimeSpan horaFin)
        {
            if (horaFin < horaInicio)
            {
                var valorLimite = horaInicio - horaFin;
                var valorAComparar = time - horaFin;
                if (valorAComparar >= valorLimite || valorAComparar <= TimeSpan.FromHours(0))
                    return true;
                return false;


            }
            else
            {
                var valorLimite = horaFin - horaInicio;
                var valorAComparar = time - horaInicio;
                if (valorAComparar <= valorLimite && valorAComparar >= TimeSpan.FromHours(0))
                    return true;
                return false;

            }
        }
    }
}



