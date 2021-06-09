using AlDente.Contracts.EstadosClientes;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.EstadosClientes;
using AlDente.Entities.EstadosClientes;
using AlDente.Services.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.EstadosClientes
{
    public class EstadoClienteService : BaseService, IEstadoClienteService
    {

        private IEstadoClienteRepository estadoClienteRepository;
        public EstadoClienteService(IUnitOfWork unitOfWork, IEstadoClienteRepository estadoClienteRepository)
            : base(unitOfWork)
        {
            estadoClienteRepository.Attach(this.unitOfWork);
            this.estadoClienteRepository = estadoClienteRepository;
        }

        public async Task Create(EstadoClienteDTO estadoClienteDto)
        {
            await estadoClienteRepository.AddAsync(new EstadoCliente
            {
                Id = estadoClienteDto.Id,
                Codigo = estadoClienteDto.Codigo,
                Descripcion = estadoClienteDto.Descripcion
            });
        }

        public async Task Delete(int id)
        {
            await estadoClienteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EstadoClienteDTO>> GetAll()
        {
            var estados = await estadoClienteRepository.GetAllAsync();
            return estados.Select(x => new EstadoClienteDTO
            {
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Id = x.Id
            });
        }

        public async Task<EstadoClienteDTO> GetById(int id)
        {
            var estado = await estadoClienteRepository.GetByIdAsync(id);
            return new EstadoClienteDTO
            {
                Codigo = estado.Codigo,
                Descripcion = estado.Descripcion,
                Id = estado.Id
            };
        }

        public async Task Update(EstadoClienteDTO estadoClienteDto)
        {
            var pepe = await estadoClienteRepository.UpdateAsync(new EstadoCliente
            {
                Codigo = estadoClienteDto.Codigo,
                Descripcion = estadoClienteDto.Descripcion,
                Id = estadoClienteDto.Id
            });
        }
    }
}
