using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlDente.Contracts.Opiniones
{
    public interface IOpinionService
    {
        Task<IEnumerable<OpinionDTO>> GetAll();

        Task Delete(int id);
        Task Create(OpinionDTO opinionDto);
        Task Update(OpinionDTO opinionDto);
    }
}
