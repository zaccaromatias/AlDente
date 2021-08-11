using AlDente.Contracts.Opiniones;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Opiniones;
using AlDente.DataAccess.Core;
using AlDente.Globalization;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Entities.Opiniones;

namespace AlDente.Services.Opiniones
{
    public class OpinionService : BaseService, IOpinionService
    {
        private IOpinionRepository opinionRepository;

        public OpinionService(IUnitOfWork unitOfWork, IOpinionRepository opinionRepository)
            : base(unitOfWork)
        {
            opinionRepository.Attach(this.unitOfWork);
            this.opinionRepository = opinionRepository;
        }


        public async Task<IEnumerable<OpinionDTO>> GetAll()
        {
            var opiniones = await opinionRepository.GetAllAsync();
            return opiniones.Select(x => new OpinionDTO
            {
                Id = x.Id,
                Texto = x.Texto,
                Calificacion = x.Calificacion,
                RestauranteId = x.RestauranteId,
                ClienteId= x.ClienteId
            });
        }

        public async Task Create(OpinionDTO opinionDto)
        {
            await Try(async () =>
            {
                await opinionRepository.AddAsync(new Opinion
                {
                    Texto = opinionDto.Texto,
                    Calificacion = opinionDto.Calificacion,
                    RestauranteId = this.unitOfWork.RestauranteId,
                    //ClienteId = this.unitOfWork.ClienteId
                });
            });
        }

        public async Task Delete(int id)
        {
            await opinionRepository.DeleteAsync(id);
        }

        public async Task Update(OpinionDTO opinionDto)
        {
            await Try(async () =>
            {
                await opinionRepository.UpdateAsync(new Opinion
                {
                    Id = opinionDto.Id,
                    Texto = opinionDto.Texto,
                    Calificacion = opinionDto.Calificacion,
                    RestauranteId = opinionDto.RestauranteId,
                    ClienteId = opinionDto.ClienteId
                });
            });
        }
    }
}
