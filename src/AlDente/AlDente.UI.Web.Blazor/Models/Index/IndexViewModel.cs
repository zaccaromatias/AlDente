using AlDente.Contracts.Beneficios;
using AlDente.UI.Web.Blazor.Data;
using AlDente.UI.Web.Blazor.Pages;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Models.Index
{
    public class IndexViewModel
    {
        IBeneficioService _beneficioService;
        public SessionData SessionData { get; private set; }

        public List<BeneficioDTO> Beneficios { get; private set; }

        public List<ToastModel> ToastModels => this.Beneficios.Select(x => new ToastModel()
        {
            ContentTemplate = RenderNotificacion(typeof(NotificacionDeBeneficio), x),
            Timeout = 0,
            ShowCloseButton = true,
            CssClass = "e-toast-success"
        }).ToList();


        private IndexViewModel(IBeneficioService beneficioService, SessionData sessionData)
        {
            this.SessionData = sessionData;
            _beneficioService = beneficioService;
            this.Beneficios = new List<BeneficioDTO>();
        }

        private RenderFragment RenderNotificacion(Type t, BeneficioDTO beneficio) => builder =>
         {
             builder.OpenComponent(0, t);
             builder.AddAttribute(1, "Beneficio", beneficio);
             builder.CloseComponent();
         };

        public async static Task<IndexViewModel> Create(IBeneficioService beneficioService, SessionData sessionData)
        {
            var model = new IndexViewModel(beneficioService, sessionData);
            await model.LoadBeneficios();
            return model;
        }


        private async Task LoadBeneficios()
        {
            if (this.SessionData != null && this.SessionData.User != null && this.SessionData.User.Id > 0)
                this.Beneficios.AddRange(await _beneficioService.GetActiveByCliente(SessionData.User.Id));
        }
    }
}
