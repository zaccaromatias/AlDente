using AlDente.Contracts.Clientes;
using AlDente.DataAccess.Sanciones;
using AlDente.Entities.Usuarios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlDente.Services.Clientes.Extensions
{
    public static class UsuarioExtensions
    {
        public async static Task<PuedeReservarDTO> PuedeReservar(this Usuario usuario, ISancionRepository sancionRepository)
        {
            List<string> motivos = new List<string>();
            if (usuario.EstadoId == (int)Usuario.Estados.Inactivo)
                motivos.Add("Te encuentras Inactivo por lo que no puedes reservar.");
            else if (usuario.EstadoId == (int)Usuario.Estados.Suspendido)
                motivos.Add("Te encuentras suspendido por lo que no puedes reservar.");
            //Valida si no hay alguna sancion Activa
            var sanciones = await sancionRepository.GetActivosByCliente(usuario.Id);
            if (sanciones.Any())
                motivos.Add("Te encuentras Sancionado por lo que no puedes reservar.");

            if (motivos.Any())
                return PuedeReservarDTO.Error(motivos);
            return PuedeReservarDTO.Success();
        }
    }
}
