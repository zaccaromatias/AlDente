using AlDente.Contracts.DiasLaborables;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.DiasLaborables;
using AlDente.Entities.DiasLaborables;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.DiasLaborables
{
    public class DiaLaboralService : BaseService, IDiaLaboralService
    {
        private IDiaLaboralRepository diaLaboralRepository;

        public DiaLaboralService(IUnitOfWork unitOfWork, IDiaLaboralRepository diaLaboralRepository)
            : base(unitOfWork)
        {
            diaLaboralRepository.Attach(this.unitOfWork);
            this.diaLaboralRepository = diaLaboralRepository;
            this.CustomValidations.Add("AK_DiaLaboral_DiaDeLaSemana", "Ya existe un horario para ese día");
        }


        public async Task<IEnumerable<DiaLaboralDTO>> GetAll()
        {
            var diasLaborables = await diaLaboralRepository.GetAllAsync();
            return diasLaborables.Select(x => new DiaLaboralDTO
            {
                Id = x.Id,
                Dia = (Contracts.DiasLaborables.DiasDeLaSemana)x.DiaDelaSemana,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                RestauranteId = x.RestauranteId
            }).OrderBy(x => x.Dia);
        }

        public async Task Create(DiaLaboralDTO diaLaboralDto)
        {
            await Try(async () =>
            {
                await diaLaboralRepository.AddAsync(new DiaLaboral
                {
                    DiaDelaSemana = (int)diaLaboralDto.Dia,
                    HoraInicio = diaLaboralDto.HoraInicio,
                    HoraFin = diaLaboralDto.HoraFin,
                    RestauranteId = this.unitOfWork.RestauranteId
                });
            });
        }

        public async Task Delete(int id)
        {
            await diaLaboralRepository.DeleteAsync(id);
        }

        public async Task Update(DiaLaboralDTO diaLaboralDto)
        {
            await Try(async () =>
            {
                await diaLaboralRepository.UpdateAsync(new DiaLaboral
                {
                    Id = diaLaboralDto.Id,
                    DiaDelaSemana = (int)diaLaboralDto.Dia,
                    HoraInicio = diaLaboralDto.HoraInicio,
                    HoraFin = diaLaboralDto.HoraFin,
                    RestauranteId = diaLaboralDto.RestauranteId
                });
            });
        }

        public async Task<IEnumerable<DiaLaboralDTO>> GetDiasLaborables()
        {
            var diasLaborables = await diaLaboralRepository.QueryAsync(x => x.RestauranteId == this.unitOfWork.RestauranteId);
            return diasLaborables.Select(x => new DiaLaboralDTO
            {
                Id = x.Id,
                Dia = (Contracts.DiasLaborables.DiasDeLaSemana)x.DiaDelaSemana,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                RestauranteId = x.RestauranteId
            }).OrderBy(x => x.Dia);
        }
    }
}
