using AlDente.DataAccess.Core;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public class EmailService : BaseService, IEmailService
    {
        private IFluentEmail email;
        public EmailService(IUnitOfWork unitOfWork, IFluentEmail email)
            : base(unitOfWork)
        {
            this.email = email;
        }

        public async Task RegistroNuevo(IEmailDataReady emailData)
        {
            await email
                .To(emailData.Addresses)
                .Subject(emailData.Subject)
                .UsingTemplateFromFile(GetTemplatePath("RegistroNuevo.cshtml"), emailData.Data)
                .SendAsync();
        }

        public async Task NuevaReserva(IEmailDataReady emailData)
        {
            await email
                .To(emailData.Addresses)
                .Subject(emailData.Subject)
                .UsingTemplateFromFile(GetTemplatePath("NuevaReserva.cshtml"), emailData.Data)
                .SendAsync();
        }

        public async Task ReservaCancelada(IEmailDataReady emailData)
        {
            await email
                .To(emailData.Addresses)
                .Subject(emailData.Subject)
                .UsingTemplateFromFile(GetTemplatePath("ReservaCancelada.cshtml"), emailData.Data)
                .SendAsync();
        }

        public async Task NuevoBeneficio(IEmailDataReady emailData)
        {
            await email
                .To(emailData.Addresses)
                .Subject(emailData.Subject)
                .UsingTemplateFromFile(GetTemplatePath("NuevoBeneficio.cshtml"), emailData.Data)
                .SendAsync();
        }

        public async Task NuevaSancion(IEmailDataReady emailData)
        {
            await email
                .To(emailData.Addresses)
                .Subject(emailData.Subject)
                .UsingTemplateFromFile(GetTemplatePath("NuevaSancion.cshtml"), emailData.Data)
                .SendAsync();
        }

        public static string GetTemplatePath(string file)
        {
            return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Templates", file);
        }
    }



    public interface IEmailDataReady
    {
        public IReadOnlyList<Address> Addresses { get; }
        public string Subject { get; }

        public object Data { get; }

        IEmailDataReady SetData(object data);
    }

    public interface IEmailDataWithAnyAddress : IEmailDataEmpty
    {
        IEmailDataReady SetSubject(string subject);
    }
    public interface IEmailDataEmpty
    {
        IEmailDataWithAnyAddress AddAddress(Address address);
    }

    public class EmailBasicData : IEmailDataEmpty, IEmailDataWithAnyAddress, IEmailDataReady
    {
        private List<Address> _Addresses;

        private object _Data;

        private string _Subject;

        public IReadOnlyList<Address> Addresses => _Addresses;

        public string Subject => _Subject;

        public object Data => _Data;

        private EmailBasicData()
        {
            this._Addresses = new List<Address>();
        }

        public static IEmailDataEmpty Create()
        {
            return new EmailBasicData();
        }

        public IEmailDataWithAnyAddress AddAddress(Address address)
        {
            this._Addresses.Add(address);
            return this;
        }

        public IEmailDataReady SetSubject(string subject)
        {
            this._Subject = subject;
            return this;
        }

        public IEmailDataReady SetData(object data)
        {
            this._Data = data;
            return this;
        }
    }

}
