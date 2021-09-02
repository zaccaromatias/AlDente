using AlDente.Contracts.Clientes;
using AlDente.Contracts.Core;
using AlDente.UI.Web.Blazor.Helpers;
using AlDente.UI.Web.Blazor.Models;
using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Services
{
    public class AuthenticationClientService : IAuthenticationClientService
    {
        private const string USER_KEY_STORAGE = "user";
        [Inject]
        protected IAuthorizationService AuthorizationService { get; set; }

        [Inject]
        private ILocalStorage LocalStorageService { get; set; }


        [Inject]
        private ClienteAuthenticationStateProvider ClienteAuthenticationStateProvider { get; set; }


        public AuthenticationClientService(IAuthorizationService authorizationService, NavigationManager navigationManager, ILocalStorage localStorage, ClienteAuthenticationStateProvider clienteAuthenticationStateProvider)
        {
            AuthorizationService = authorizationService;
            LocalStorageService = localStorage;
            ClienteAuthenticationStateProvider = clienteAuthenticationStateProvider;

        }


        public async Task Initialize()
        {
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        public async Task Login(LoginViewModel model)
        {
            var user = await AuthorizationService.Login(new LoginDTO
            {
                Email = model.Email,
                Password = model.Password
            });

            await LocalStorageService.SetItem(USER_KEY_STORAGE, user);
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();

        }

        public async Task Logout()
        {
            await LocalStorageService.RemoveItem(USER_KEY_STORAGE);
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        public async Task Register(RegisterModel registerModel)
        {
            var user = await AuthorizationService.Register(new ClienteRegisterDTO
            {
                Apellido = registerModel.Apellido,
                DNI = registerModel.DNI,
                Email = registerModel.Email,
                Nombre = registerModel.Nombre,
                Password = registerModel.Password,
                Telefono = registerModel.Telefono
            });

            await LocalStorageService.SetItem(USER_KEY_STORAGE, user);
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
    }
}
