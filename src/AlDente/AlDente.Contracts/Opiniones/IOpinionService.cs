using System.Threading.Tasks;

namespace AlDente.Contracts.Opiniones
{
    public interface IOpinionService
    {
        Task<OpinionesCollectionsDTO> GetAll();

        Task<OpinionesCollectionsDTO> GetOpinionesPublicadas();

        Task<OpinionesCollectionsDTO> GetOpinionesDelCliente(int clienteId);

        Task<OpinionesCollectionsDTO> GetMoreOpinionesPublicadas(MasOpinionesParameterDTO masOpinionesParameterDTO);

        Task Delete(int id);
        Task Create(OpinionDTO opinionDto);
        Task Update(OpinionDTO opinionDto);

        Task MarcarOpinionComo(EstadosDeUnOpinion estado, int opinionId, int userId, string motivo);

        Task<OpinionesCollectionsDTO> GetRespuestas(MasOpinionesParameterDTO masOpinionesParameterDTO);
        Task<OpinionesCollectionsDTO> GetMoreRespuestas(MasOpinionesParameterDTO masOpinionesParameterDTO);
    }
}
