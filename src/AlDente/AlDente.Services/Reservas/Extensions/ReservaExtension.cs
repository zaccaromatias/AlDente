using AlDente.Contracts.Reservas;
using AlDente.DataAccess.Turnos;
using AlDente.DataAccess.Usuarios;
using AlDente.Entities.Reservas;
using System.Threading.Tasks;

namespace AlDente.Services.Reservas.Extensions
{
    public static class ReservaExtension
    {
        public async static Task<ReservaBasicDTO> MapToBasicDTO(this Reserva x, ITurnoRepository turnoRepository, IUsuarioRepository usuarioRepository)
        {
            var turno = (await turnoRepository.GetByIdAsync(x.TurnoId)).Text;
            var usuario = await usuarioRepository.GetByIdAsync(x.ClienteId);
            var dto = new ReservaBasicDTO
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Comensales = x.CantidadComensales,
                EstadoId = x.EstadoReservaId,
                Fecha = x.FechaReserva,
                FechaDeCreacion = x.FechaCreacion,
                LimiteDeHora = ReservaService.LIMITE_DE_HORAS_DONDE_NO_SE_PUEDE_CANCELAR,
                Turno = turno,
                EmailUsuario = usuario.Email,
                NombreUsuario = usuario.Caption,
                MotivoCancelacion = x.MotivoCancelacion
            };
            return dto;
        }
    }
}
