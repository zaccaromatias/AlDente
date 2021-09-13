
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Sanciones
{
    public interface ITipoSancionService
    {
        Task<IEnumerable<TipoSancionDTO>> GetAll();
        Task Delete(int id);
        Task Create(TipoSancionDTO tipoSancionDto);
        Task Update(TipoSancionDTO tipoSancionDto);
    }
}
