using AlDente.Contracts.Opiniones;
using AlDente.UI.Web.Blazor.Data;
using System;

namespace AlDente.UI.Web.Blazor.Models.Opinion
{
    public class OpinionViewModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int Root => this.OpinionPrincipalId > 0 ? this.OpinionPrincipalId.Value : this.Id;

        public string ClienteEmail { get; set; }
        public int Calificacion { get; set; }
        public string Texto { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PublishDate { get; set; }

        public int? OpinionPrincipalId { get; set; }
        public string DateCaption => this.Date.ToString("dd/MM/yyyy HH:mm");

        public bool EsQuienHizoLaOpinion => this.Session.User.Id == this.ClienteId;

        public bool EsEmpleado => this.Session.User.Roles.Contains("Empleado");

        public bool ShowEditButton => EsQuienHizoLaOpinion && this.Estado != EstadosDeUnOpinion.Publicado;
        public bool ShowRemoveButton => EsQuienHizoLaOpinion;
        public bool ShowRespondButton => true;
        public bool ShowPublishButton => this.EsEmpleado && this.Estado != EstadosDeUnOpinion.Publicado;

        public bool ShowUnPublishButton => this.EsEmpleado && this.Estado == EstadosDeUnOpinion.Publicado;

        public bool ShowMarkInapropiateButton => this.EsEmpleado && this.Estado != EstadosDeUnOpinion.Publicado && this.Estado != EstadosDeUnOpinion.Inapropiado;

        public SessionData Session { get; set; }

        public string EmailCaption => this.ClienteEmail.Substring(0, this.ClienteEmail.IndexOf("@"));

        public int EstadoId { get; set; }
        public bool TieneRespuestas { get; set; }

        public bool VerRespuestas { get; set; }

        public bool EsRespuesta => this.OpinionPrincipalId > 0;

        public EstadosDeUnOpinion Estado => (EstadosDeUnOpinion)this.EstadoId;

        const string ESTRELLA = "Estrella";
        const string ESTRELLAS = "Estrellas";
        public string CalificacionCaption
        {
            get
            {
                if (this.Calificacion > 1)
                    return $"{this.Calificacion} {ESTRELLAS}";
                return $"{this.Calificacion} {ESTRELLA}";
            }
        }

        public string GetEstadoCaption()
        {
            switch (this.Estado)
            {
                case EstadosDeUnOpinion.Nuevo:
                    return "Nuevo";
                case EstadosDeUnOpinion.Publicado:
                    return "Publicado";
                case EstadosDeUnOpinion.Removido:
                    return "Removido";
                case EstadosDeUnOpinion.Inapropiado:
                    return "Inapropiado";
                default:
                    return "";
            }

        }
        public string GetTypeOfBadgeByState()
        {
            switch (this.Estado)
            {
                case EstadosDeUnOpinion.Nuevo:
                    return "badge-warning";
                case EstadosDeUnOpinion.Publicado:
                    return "badge-success";
                case EstadosDeUnOpinion.Removido:
                case EstadosDeUnOpinion.Inapropiado:
                    return "badge-aldente";
                default:
                    return "badge-warning";
            }

        }
        public string GetTypeOfBadgeByCalificacion()
        {
            switch (Calificacion)
            {
                case 6:
                case 5:
                case 4:
                    return "badge-success";
                case 3:
                    return "badge-warning";
                default:
                    return "badge-aldente";

            }
        }
    }
}
