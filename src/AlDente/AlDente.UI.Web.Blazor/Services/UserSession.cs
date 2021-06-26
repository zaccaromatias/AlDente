using AlDente.Contracts.Clientes;

namespace AlDente.UI.Web.Blazor.Services
{
    public class UserSession
    {
        public ClienteBasicDTO User { get; private set; }

        private UserSession()
        {

        }
        private static UserSession _instance;
        public static UserSession Data
        {
            get
            {
                if (_instance == null)
                    _instance = new UserSession();
                return _instance;
            }
        }

        public static void LoadUser(ClienteBasicDTO user)
        {
            Data.User = user;
        }

    }
}
