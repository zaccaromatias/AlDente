using AlDente.Contracts.Core;
using AlDente.Contracts.Sanciones;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Sanciones;
using AlDente.Entities.Sanciones;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlDente.Services.Sanciones
{
    public class TipoSancionService : BaseService, ITipoSancionService
    {
        private ITipoSancionRepository tipoSancionRepository;

        public TipoSancionService(IUnitOfWork unitOfWork, ITipoSancionRepository tsRepository)
            : base(unitOfWork)
        {
            tsRepository.Attach(this.unitOfWork);
            this.tipoSancionRepository = tsRepository;
            this.CustomValidations.Add("UK_TipoSancion_Codigo", "Ya existe un tipo de sanción con ese Código");
        }
        public async Task<IEnumerable<TipoSancionDTO>> GetAll()
        {
            var tiposSancion = await tipoSancionRepository.GetAllAsync();
            return tiposSancion.Select(x => new TipoSancionDTO
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                DiasSuspension = x.DiasSuspension
            });
        }
        public async Task Create(TipoSancionDTO tipoSancionDTO)
        {
            await Try(async () =>
            {
                await tipoSancionRepository.AddAsync(new TipoSancion
                {
                    Codigo = tipoSancionDTO.Codigo,
                    Descripcion = tipoSancionDTO.Descripcion,
                    DiasSuspension = tipoSancionDTO.DiasSuspension
                });
            });
        }

        public async Task Delete(int id)
        {
            await tipoSancionRepository.DeleteAsync(id);
        }

        public async Task Update(TipoSancionDTO tipoSancionDto)
        {
            await Try(async () =>
            {
                await tipoSancionRepository.UpdateAsync(new TipoSancion
                {
                    Id = tipoSancionDto.Id,
                    Codigo = tipoSancionDto.Codigo,
                    Descripcion = tipoSancionDto.Descripcion,
                    DiasSuspension = tipoSancionDto.DiasSuspension
                });
            });
        }

    }
}
