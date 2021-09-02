using AlDente.Contracts.Core;
using AlDente.UI.Web.Blazor.Data;
using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Helpers
{
    public class ClienteAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject]
        private ILocalStorage LocalStorage { get; set; }
        [Inject]
        private SessionData _sessionData { get; set; }
        public static bool IsAuthenticating { get; set; }

        public ClienteAuthenticationStateProvider(ILocalStorage localStorage, SessionData sessionData)
        {
            LocalStorage = localStorage;
            _sessionData = sessionData;
        }
        public async Task<IAuthorizationEntity> GetUserAsync()
        {
            return await LocalStorage.GetItem<AuthorizationEntityDTO>("user");
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (IsAuthenticating)
            {

                _sessionData.User = null;
                return null;
            }

            else
            {
                IAuthorizationEntity user = await GetUserAsync();
                if (user != null)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim(ClaimTypes.Email, user.Email)
                        };
                    foreach (var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    identity = new ClaimsIdentity(claims, "WebApiAuth");
                    _sessionData.User = user;
                }
                else
                {
                    identity = new ClaimsIdentity();
                    _sessionData.User = null;
                }
            }
            var result = await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            return result;
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
