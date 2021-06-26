using AlDente.UI.Web.Blazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Helpers
{
    public class AppRouteView : RouteView
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IAuthenticationClientService AuthenticationService { get; set; }


        protected override void Render(RenderTreeBuilder builder)
        {
            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;

            if (authorize && AuthenticationService.Session.User == null)
            {
                var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
                NavigationManager.NavigateTo($"login?returnUrl={returnUrl}");
            }
            else
            {
                base.Render(builder);
            }
        }
    }

    public class ClienteAuthenticationStateProvider : AuthenticationStateProvider
    {
        public static bool IsAuthenticating { get; set; }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (IsAuthenticating)
            {
                return null;
            }
            else if (UserSession.Data.User != null)
            {
                identity = new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, UserSession.Data.User.Email)

                        }, "WebApiAuth");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public class AppState
    {
        public event Action RefreshRequested;

        public void TriggerRefreshAction()
        {
            StateChanged();
        }

        private void StateChanged()
        {
            RefreshRequested?.Invoke();
            Console.WriteLine("State has changed");
        }
    }

    public class MyAuthorizeView : AuthorizeView
    {
    }
}
