using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public interface IEmailService
    {
        Task RegistroNuevo(IEmailDataReady emailData);
        Task NuevaReserva(IEmailDataReady emailData);
        Task ReservaCancelada(IEmailDataReady emailData);
        Task NuevoBeneficio(IEmailDataReady emailData);
        Task NuevaSancion(IEmailDataReady emailData);

        Task OpinionPublicada(IEmailDataReady emailData);
    }
}
