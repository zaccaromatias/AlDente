using AlDente.Contracts.Mesas;
using AlDente.Contracts.Core;
using AlDente.DataAccess.Mesas;
using AlDente.DataAccess.Core;
using AlDente.Globalization;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlDente.Entities.Mesas;

namespace AlDente.Services.Mesas
{
    public class MesaService : BaseService, IMesaService
    {
        private IMesaRepository mesaRepository;

        public MesaService(IUnitOfWork unitOfWork, IMesaRepository mesaRepository)
            : base(unitOfWork)
        {
            mesaRepository.Attach(this.unitOfWork);
            this.mesaRepository = mesaRepository;
        }


        public async Task<IEnumerable<MesaDTO>> GetAll()
        {
            var mesas = await mesaRepository.GetAllAsync();
            return mesas.Select(x => new MesaDTO
            {
                Id = x.Id,
                Capacidad = x.Capacidad
            });
        }

        public async Task Create(MesaDTO mesaDto)
        {
            await Try(async () =>
            {
                 await mesaRepository.AddAsync(new Mesa
                {
                    Capacidad = mesaDto.Capacidad
                });
            });
        }

        public async Task Delete(int id)
        {
            await mesaRepository.DeleteAsync(id);
        }

        public async Task Update(MesaDTO mesaDto)
        {
            await Try(async () =>
            {
                await mesaRepository.UpdateAsync(new Mesa
                {
                    Id = mesaDto.Id,
                    Capacidad = mesaDto.Capacidad
                });
            });
        }
    }
}
