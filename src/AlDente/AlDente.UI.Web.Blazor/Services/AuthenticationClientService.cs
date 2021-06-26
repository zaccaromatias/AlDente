using AlDente.Contracts.Clientes;
using AlDente.UI.Web.Blazor.Helpers;
using AlDente.UI.Web.Blazor.Models;
using BlazorBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Services
{
    public class AuthenticationClientService : IAuthenticationClientService
    {
        [Inject]
        protected IClienteService ClienteService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        [Inject]
        private ILocalStorage LocalStorageService { get; set; }

        [Inject]
        private ClienteAuthenticationStateProvider ClienteAuthenticationStateProvider { get; set; }
        public UserSession Session => UserSession.Data;

        public AuthenticationClientService(IClienteService clienteService, NavigationManager navigationManager, ILocalStorage localStorage, ClienteAuthenticationStateProvider clienteAuthenticationStateProvider)
        {
            ClienteService = clienteService;
            NavigationManager = navigationManager;
            LocalStorageService = localStorage;
            ClienteAuthenticationStateProvider = clienteAuthenticationStateProvider;

        }


        public async Task Initialize()
        {
            UserSession.LoadUser(await LocalStorageService.GetItem<ClienteBasicDTO>("user"));
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        public async Task Login(string email, string password)
        {
            var user = await ClienteService.Login(new LoginDTO
            {
                Email = email,
                Password = password
            });
            UserSession.LoadUser(user);
            await LocalStorageService.SetItem("user", user);
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();

        }

        public async Task Logout()
        {
            UserSession.LoadUser(null);
            await LocalStorageService.RemoveItem("user");
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        public async Task Register(RegisterModel registerModel)
        {
            var user = await ClienteService.Register(new ClienteRegisterDTO
            {
                Apellido = registerModel.Apellido,
                DNI = registerModel.DNI,
                Email = registerModel.Email,
                Nombre = registerModel.Nombre,
                Password = registerModel.Password,
                Telefono = registerModel.Telefono
            });
            UserSession.LoadUser(user);
            await LocalStorageService.SetItem("user", user);
            ClienteAuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
    }
}
