using LabPortugal_Intranet.Models;
using System.Text.Json;

namespace LabPortugal_Intranet.Services
{
    public class GoogleFarmaciaEnSesionService
    {

        public static GoogleFarmacia getSessionGoogleFarmacia(HttpContext context)
        {
            string cadenaGoogleFarmacia = context.Session.GetString("googleFarmacia");

            GoogleFarmacia farmaciaGoogleEnSesion = JsonSerializer.Deserialize<GoogleFarmacia>(cadenaGoogleFarmacia);
            return farmaciaGoogleEnSesion;
        }

        public static void setSessionGoogleFarmacia(HttpContext context, GoogleFarmacia farmacia)
        {
            string farmaciaGoogleEnSesion = JsonSerializer.Serialize(farmacia);
            context.Session.SetString("googleFarmacia", farmaciaGoogleEnSesion);
        }

    }
}
