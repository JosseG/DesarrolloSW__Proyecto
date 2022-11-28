using LabPortugal_Intranet.Models;
using System.Text.Json;

namespace LabPortugal_Intranet.Services
{
    public class UsuarioEnSesionService
    {

        public static UsuarioFarmacia getSessionUser(HttpContext context)
        {
            String cadenaUsuario = context.Session.GetString("usuario");

            UsuarioFarmacia usuarioEnSesion = JsonSerializer.Deserialize<UsuarioFarmacia>(cadenaUsuario);
            return usuarioEnSesion;
        }

        public static void setSessionUser(HttpContext context, UsuarioFarmacia user)
        {
            string usuarioEnSesion = JsonSerializer.Serialize(user);
            context.Session.SetString("usuario", usuarioEnSesion);
        }

    }
}
