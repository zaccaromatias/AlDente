using AlDente.Contracts.Core;
using AlDente.Contracts.Beneficios;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Beneficios;
using AlDente.Entities.Beneficios;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlDente.Services.Sanciones
{
    public class TipoBeneficioService : BaseService, ITipoBeneficioService
    {
        private ITipoBeneficioRepository tipoBeneficioRepository;

        public TipoBeneficioService(IUnitOfWork unitOfWork, ITipoBeneficioRepository tbRepository)
            : base(unitOfWork)
        {
            tbRepository.Attach(this.unitOfWork);
            this.tipoBeneficioRepository = tbRepository;
            this.CustomValidations.Add("UK_TipoBeneficio_Codigo", "Ya existe un tipo de beneficio con ese Código");
        }
        public async Task<IEnumerable<TipoBeneficioDTO>> GetAll()
        {
            var tiposSancion = await tipoBeneficioRepository.GetAllAsync();
            return tiposSancion.Select(x => new TipoBeneficioDTO
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Descuento  = x.Descuento
            });
        }
        public async Task Create(TipoBeneficioDTO tipoBeneficioDTO)
        {
            await Try(async () =>
            {
                await tipoBeneficioRepository.AddAsync(new TipoBeneficio
                {
                    Codigo = tipoBeneficioDTO.Codigo,
                    Descripcion = tipoBeneficioDTO.Descripcion,
                    Descuento = tipoBeneficioDTO.Descuento
                });
            });
        }

        public async Task Delete(int id)
        {
            await tipoBeneficioRepository.DeleteAsync(id);
        }

        public async Task Update(TipoBeneficioDTO tipoBeneficioDto)
        {
            await Try(async () =>
            {
                await tipoBeneficioRepository.UpdateAsync(new TipoBeneficio
                {
                    Id = tipoBeneficioDto.Id,
                    Codigo = tipoBeneficioDto.Codigo,
                    Descripcion = tipoBeneficioDto.Descripcion,
                    Descuento = tipoBeneficioDto.Descuento
                });
            });
        }

    }
}
