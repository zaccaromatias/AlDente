using System.Threading.Tasks;

namespace AlDente.UI.Web.Blazor.Services
{
    public interface IAuthenticationClientService
    {
        UserSession Session { get; }
        Task Initialize();
        Task Login(string email, string password);

        Task Register(AlDente.UI.Web.Blazor.Models.RegisterModel registerModel);
        Task Logout();
    }
}
