using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Restaurantes
{
    public interface IRestauranteService
    {
        Task<IEnumerable<RestauranteDTO>> GetAll();

        Task Create(RestauranteDTO restauranteDTO);

        Task Update(RestauranteDTO restauranteDTO);

        Task Delete(int id);

        Task<string> GetUrlMenu();
    }
}
