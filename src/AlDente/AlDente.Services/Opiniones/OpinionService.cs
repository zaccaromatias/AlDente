using AlDente.Contracts.Core;
using AlDente.Contracts.Opiniones;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Opiniones;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Opiniones;
using AlDente.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Opiniones
{
    public class OpinionService : BaseService, IOpinionService
    {
        private const int CANTIDAD_A_MOSTRAR = 5;
        private IOpinionRepository opinionRepository;
        private IUsuarioRepository usuarioRepository;

        public OpinionService(IUnitOfWork unitOfWork, IOpinionRepository opinionRepository, IUsuarioRepository usuarioRepository)
            : base(unitOfWork)
        {
            opinionRepository.Attach(this.unitOfWork);
            usuarioRepository.Attach(this.unitOfWork);
            this.opinionRepository = opinionRepository;
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<OpinionesCollectionsDTO> GetAll()
        {
            var opiniones = await opinionRepository.QueryAsync(x => x.EstadoId != (int)EstadosDeUnOpinion.Removido && x.OpinionPrincipalId == null);
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, false, false);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = dtos.Count(),
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }


        public async Task<OpinionesCollectionsDTO> GetOpinionesDelCliente(int clienteId)
        {
            var cliente = await usuarioRepository.GetByIdAsync(clienteId);
            var opiniones = await opinionRepository.QueryAsync(x => x.ClienteId == clienteId && x.EstadoId != (int)EstadosDeUnOpinion.Removido && x.OpinionPrincipalId == null);
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, cliente.TipoUsuarioId == (int)TipoDeUsuarios.Cliente, false);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = dtos.Count(),
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }
        public async Task<OpinionesCollectionsDTO> GetOpinionesPublicadas()
        {
            var opiniones = await opinionRepository.QueryAsync(x => x.EstadoId == (int)EstadosDeUnOpinion.Publicado && x.OpinionPrincipalId == null);
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, true, false);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = dtos.Count(),
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }

        private async Task<List<OpinionDTO>> MapToOpinionDTO(IEnumerable<Opinion> opiniones, bool soloRespuestasPublicadas, bool orderAsc)
        {
            var dtos = opiniones.Select(x => new OpinionDTO
            {
                Id = x.Id,
                Texto = x.Texto,
                Calificacion = x.Calificacion,
                RestauranteId = x.RestauranteId,
                ClienteId = x.ClienteId,
                EmpleadoAprovadorId = x.EmpleadoAprovadorId,
                EstadoId = x.EstadoId,
                Fecha = x.Fecha,
                FechaAprovacion = x.FechaAprovacion,
                OpinionPrincipalId = x.OpinionPrincipalId,
                RemovidoFecha = x.RemovidoFecha,
                RemovidoMotivo = x.RemovidoMotivo,
                RemovidoPor = x.RemovidoPor
            }).ToList();

            if (orderAsc)
                dtos = dtos.OrderBy(x => x.Fecha).ToList();


            else
                dtos = dtos.OrderByDescending(x => x.Fecha).ToList();

            await SetCalculatedValues(dtos, soloRespuestasPublicadas);
            return dtos;
        }

        private async Task SetCalculatedValues(IEnumerable<OpinionDTO> dtos, bool soloRespuestasPublicadas)
        {
            foreach (var opinion in dtos)
            {
                var cliente = await usuarioRepository.GetByIdAsync(opinion.ClienteId);
                opinion.ClienteEmail = cliente.Email;
                opinion.TieneRespuestas = (await opinionRepository.QueryAsync(x => x.EstadoId != (int)EstadosDeUnOpinion.Removido && x.OpinionPrincipalId == opinion.Id)).Any();
            }
        }

        public async Task<OpinionesCollectionsDTO> GetMoreOpinionesPublicadas(MasOpinionesParameterDTO masOpinionesParameterDTO)
        {
            var cliente = await usuarioRepository.GetByIdAsync(masOpinionesParameterDTO.ClienteId);

            IEnumerable<Opinion> opiniones;
            if (masOpinionesParameterDTO.SoloMisOpiniones)
                opiniones = await opinionRepository.QueryAsync(x => x.Fecha < masOpinionesParameterDTO.FechaDeLaUltimaOpinionCargada && x.ClienteId == masOpinionesParameterDTO.ClienteId && x.EstadoId == (int)EstadosDeUnOpinion.Removido && x.OpinionPrincipalId == null);
            else if (cliente.TipoUsuarioId == (int)TipoDeUsuarios.Empleado)
                opiniones = await opinionRepository.QueryAsync(x => x.Fecha < masOpinionesParameterDTO.FechaDeLaUltimaOpinionCargada && x.OpinionPrincipalId == null);
            else
                opiniones = await opinionRepository.QueryAsync(x => x.EstadoId == (int)EstadosDeUnOpinion.Publicado && x.Fecha < masOpinionesParameterDTO.FechaDeLaUltimaOpinionCargada && x.OpinionPrincipalId == null);
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, cliente.TipoUsuarioId == (int)TipoDeUsuarios.Cliente, false);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = masOpinionesParameterDTO.CantidadTotalDeOpiniones,
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }

        public async Task<OpinionesCollectionsDTO> GetRespuestas(MasOpinionesParameterDTO masOpinionesParameterDTO)
        {
            var cliente = await usuarioRepository.GetByIdAsync(masOpinionesParameterDTO.ClienteId);
            IEnumerable<Opinion> opiniones;
            if (cliente.TipoUsuarioId == (int)TipoDeUsuarios.Empleado)
            {
                opiniones = await opinionRepository.QueryAsync(x => x.OpinionPrincipalId == masOpinionesParameterDTO.OpinionPrincipalId);
            }
            else
            {
                opiniones = await opinionRepository.QueryAsync(x => x.EstadoId == (int)EstadosDeUnOpinion.Publicado
                && x.OpinionPrincipalId == masOpinionesParameterDTO.OpinionPrincipalId);
            }
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, cliente.TipoUsuarioId == (int)TipoDeUsuarios.Empleado, true);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = dtos.Count(),
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }

        public async Task<OpinionesCollectionsDTO> GetMoreRespuestas(MasOpinionesParameterDTO masOpinionesParameterDTO)
        {
            var cliente = await usuarioRepository.GetByIdAsync(masOpinionesParameterDTO.ClienteId);
            IEnumerable<Opinion> opiniones;
            if (cliente.TipoUsuarioId == (int)TipoDeUsuarios.Empleado)
            {
                opiniones = await opinionRepository.QueryAsync(x => x.Fecha < masOpinionesParameterDTO.FechaDeLaUltimaOpinionCargada && x.OpinionPrincipalId == masOpinionesParameterDTO.OpinionPrincipalId);
            }
            else
            {
                opiniones = await opinionRepository.QueryAsync(x => x.Fecha < masOpinionesParameterDTO.FechaDeLaUltimaOpinionCargada && x.EstadoId == (int)EstadosDeUnOpinion.Publicado
                && x.OpinionPrincipalId == masOpinionesParameterDTO.OpinionPrincipalId);
            }
            List<OpinionDTO> dtos = await MapToOpinionDTO(opiniones, cliente.TipoUsuarioId == (int)TipoDeUsuarios.Cliente, true);
            return new OpinionesCollectionsDTO
            {
                CantidadACargar = CANTIDAD_A_MOSTRAR,
                CantidadTotalDeOpiniones = masOpinionesParameterDTO.CantidadTotalDeOpiniones,
                Opiniones = dtos.Take(CANTIDAD_A_MOSTRAR)
            };
        }

        public async Task Create(OpinionDTO opinionDto)
        {
            await Try(async () =>
            {
                var cliente = await usuarioRepository.GetByIdAsync(opinionDto.ClienteId);
                var now = DateTime.Now;
                await opinionRepository.AddAsync(new Opinion
                {
                    Texto = opinionDto.Texto,
                    Calificacion = opinionDto.Calificacion,
                    RestauranteId = this.unitOfWork.RestauranteId,
                    ClienteId = opinionDto.ClienteId,
                    EstadoId = (TipoDeUsuarios)cliente.TipoUsuarioId == TipoDeUsuarios.Empleado ? (int)EstadosDeUnOpinion.Publicado : (int)EstadosDeUnOpinion.Nuevo,
                    Fecha = now,
                    FechaAprovacion = (TipoDeUsuarios)cliente.TipoUsuarioId == TipoDeUsuarios.Empleado ? now : (DateTime?)null,
                    EmpleadoAprovadorId = (TipoDeUsuarios)cliente.TipoUsuarioId == TipoDeUsuarios.Empleado ? opinionDto.ClienteId : (int?)null,
                    OpinionPrincipalId = opinionDto.OpinionPrincipalId > 0 ? opinionDto.OpinionPrincipalId : (int?)null,
                }); ;
            });
        }

        public async Task MarcarOpinionComo(EstadosDeUnOpinion estado, int opinionId, int userId, string motivo)
        {
            await Try(async () =>
            {
                var opinion = await opinionRepository.GetByIdAsync(opinionId);
                if (opinion != null)
                {
                    opinion.EstadoId = (int)estado;
                    if (estado == EstadosDeUnOpinion.Publicado)
                    {
                        opinion.EmpleadoAprovadorId = userId;
                        opinion.FechaAprovacion = DateTime.Now;
                    }
                    else if (estado == EstadosDeUnOpinion.Nuevo)
                    {
                        opinion.EmpleadoAprovadorId = null;
                        opinion.FechaAprovacion = null;
                    }

                    else if (estado == EstadosDeUnOpinion.Inapropiado)
                    {
                        opinion.EmpleadoAprovadorId = null;
                        opinion.FechaAprovacion = null;
                        opinion.RemovidoPor = userId;
                        opinion.RemovidoFecha = DateTime.Now;
                        opinion.RemovidoMotivo = motivo;
                    }
                    await opinionRepository.UpdateAsync(opinion);
                }
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
