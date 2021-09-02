using AlDente.UI.Web.Blazor.Models;
using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Services
{
    public interface IAuthenticationClientService
    {
       
        Task Initialize();
        Task Login(LoginViewModel model);

        Task Register(RegisterModel registerModel);
        Task Logout();
    }
}
