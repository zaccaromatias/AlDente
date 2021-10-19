using AlDente.Contracts.Core;
using AlDente.DataAccess.Core;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Usuarios;
using AlDente.Globalization;
using FluentEmail.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Core
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        private IUsuarioRepository usuarioRepository;
        private IEmailService emailService;

        public AuthorizationService(IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository, IEmailService emailService)
            : base(unitOfWork)
        {
            usuarioRepository.Attach(this.unitOfWork);
            this.usuarioRepository = usuarioRepository;
            this.CustomValidations.Add("AK_Cliente_Email", Messages.AK_Cliente_Email);
            this.CustomValidations.Add("AK_Cliente_DNI", Messages.AK_Cliente_DNI);
            this.emailService = emailService;
        }
        public async Task<IAuthorizationEntity> Login(LoginDTO loginDTO)
        {

            var usuario = (await usuarioRepository.QueryAsync(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password)).SingleOrDefault();

            if (usuario == null)
                throw new DomainException(Messages.EmailOrPasswordWasNotCorrect);
            var result = new AuthorizationEntityDTO(usuario.Id, usuario.TipoUsuarioId, usuario.EstadoId, usuario.Email);



            return result;
        }

        public async Task<IAuthorizationEntity> Register(RegisterDTO dto)
        {
            return await this.Try(async () =>
            {
                var id = await usuarioRepository.AddAsync(new Usuario
                {
                    Apellido = dto.Apellido,
                    DNI = dto.DNI,
                    Email = dto.Email,
                    EstadoId = dto.UsuarioCreadorId.HasValue ? (int)EstadosDeUnUsuario.Nuevo : (int)EstadosDeUnUsuario.Activo,
                    Nombre = dto.Nombre,
                    Password = dto.Password,
                    Telefono = dto.Telefono,
                    UsuarioCreadorId = dto.UsuarioCreadorId,
                    FechaCreacion = DateTime.Now,
                    TipoUsuarioId = (int)TipoDeUsuarios.Cliente

                });
                if (id == 0)
                    throw new DomainException("No se pudo Crear");
                await NotificarNuevoRegistro(dto);
                return await GetUsuarioBasicDTO(id);
            });

        }

        private async Task NotificarNuevoRegistro(RegisterDTO dto)
        {
            var emailData = EmailBasicData.Create()
                                .AddAddress(new Address(emailAddress: dto.Email, null))
                                .SetSubject("Gracias Por Registrarte en AlDente")
                                .SetData(new { Cliente = $"{dto.Nombre} {dto.Apellido}", URL = this.unitOfWork.URL });
            await emailService.RegistroNuevo(emailData);
        }

        private async Task<IAuthorizationEntity> GetUsuarioBasicDTO(int id)
        {
            var usuario = await usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new DomainException("Cliente No encontrado");
            return new AuthorizationEntityDTO(usuario.Id, usuario.TipoUsuarioId, usuario.EstadoId, usuario.Email);
        }

    }
}
