using AlDente.UI.Web.Blazor.Models;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Services
{
    public interface IAuthenticationClientService
    {
        UserSession Session { get; }
        Task Initialize();
        Task Login(LoginViewModel model);

        Task Register(RegisterModel registerModel);
        Task Logout();
    }
}
