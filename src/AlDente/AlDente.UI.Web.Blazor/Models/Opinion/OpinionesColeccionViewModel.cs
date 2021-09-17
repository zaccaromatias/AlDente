using AlDente.Contracts.Opiniones;
using AlDente.UI.Web.Blazor.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Opinion
{
    public class OpinionesColeccionViewModel
    {
        IOpinionService _opinionService;
        public List<OpinionViewModel> Opiniones { get; set; }
        public int CantidadTotalDeOpiniones { get; private set; }
        public SessionData Session { get; private set; }

        public bool ShowButtonLoadMore { get; private set; }
        public bool SoloMisOpiniones { get; private set; }
        public int? OpinionPrincipalId { get; private set; }

        private OpinionesColeccionViewModel(IOpinionService opinionService, SessionData session, bool soloMisOpiniones, int? opinionPrincipalId)
        {
            this.Opiniones = new List<OpinionViewModel>();
            this.SoloMisOpiniones = soloMisOpiniones;
            this.OpinionPrincipalId = opinionPrincipalId;
            _opinionService = opinionService;
            Session = session;
        }


        public static async Task<OpinionesColeccionViewModel> Create(IOpinionService opinionService, SessionData session, bool soloMisOpiniones, int? opinionPrincipalId)
        {
            var model = new OpinionesColeccionViewModel(opinionService, session, soloMisOpiniones, opinionPrincipalId);
            await model.LoadOpiniones();
            return model;
        }

        private async Task LoadOpiniones()
        {
            this.Opiniones.Clear();
            OpinionesCollectionsDTO result;

            if (OpinionPrincipalId.HasValue)
            {
                result = await _opinionService.GetRespuestas(new MasOpinionesParameterDTO
                {
                    ClienteId = Session.User.Id,
                    OpinionPrincipalId = OpinionPrincipalId.Value
                });
            }
            else if (SoloMisOpiniones)
                result = await _opinionService.GetOpinionesDelCliente(Session.User.Id);
            else if (Session.User.Roles.Contains("Empleado"))
                result = await _opinionService.GetAll();
            else
                result = await _opinionService.GetOpinionesPublicadas();
            this.CantidadTotalDeOpiniones = result.CantidadTotalDeOpiniones;
            this.Opiniones.AddRange(result.Opiniones.Select(o => new OpinionViewModel
            {
                Id = o.Id,
                ClienteEmail = o.ClienteEmail,
                Date = o.Fecha,
                ClienteId = o.ClienteId,
                EstadoId = o.EstadoId,
                Calificacion = o.Calificacion,
                PublishDate = o.FechaAprovacion,
                Texto = o.Texto,
                Session = Session,
                TieneRespuestas = o.TieneRespuestas,
                OpinionPrincipalId = o.OpinionPrincipalId
            }));
            this.ShowButtonLoadMore = this.Opiniones.Count() != this.CantidadTotalDeOpiniones;
        }

        public async Task LoadMasOpiniones()
        {
            OpinionesCollectionsDTO result;
            if (OpinionPrincipalId.HasValue)
            {
                result = await _opinionService.GetMoreRespuestas(new MasOpinionesParameterDTO
                {
                    CantidadTotalDeOpiniones = this.CantidadTotalDeOpiniones,
                    FechaDeLaUltimaOpinionCargada = this.Opiniones.OrderByDescending(x => x.Date).Last().Date,
                    ClienteId = this.Session.User.Id,
                    OpinionPrincipalId = this.OpinionPrincipalId.Value
                });
            }
            else
            {
                result = await _opinionService.GetMoreOpinionesPublicadas(new MasOpinionesParameterDTO
                {
                    CantidadTotalDeOpiniones = this.CantidadTotalDeOpiniones,
                    FechaDeLaUltimaOpinionCargada = this.Opiniones.OrderByDescending(x => x.Date).Last().Date,
                    ClienteId = this.Session.User.Id,
                    SoloMisOpiniones = this.SoloMisOpiniones
                });
            }

            this.Opiniones.AddRange(result.Opiniones.Select(o => new OpinionViewModel
            {
                Id = o.Id,
                ClienteEmail = o.ClienteEmail,
                Date = o.Fecha,
                ClienteId = o.ClienteId,
                EstadoId = o.EstadoId,
                Calificacion = o.Calificacion,
                PublishDate = o.FechaAprovacion,
                Texto = o.Texto,
                Session = Session,
                TieneRespuestas = o.TieneRespuestas,
                OpinionPrincipalId = o.OpinionPrincipalId
            }));
            this.ShowButtonLoadMore = this.Opiniones.Count() != this.CantidadTotalDeOpiniones;
        }
    }
}
