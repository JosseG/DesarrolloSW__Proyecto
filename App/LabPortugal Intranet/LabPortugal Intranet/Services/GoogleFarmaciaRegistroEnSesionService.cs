using LabPortugal_Intranet.Models;
using System.Text.Json;

namespace LabPortugal_Intranet.Services
{
    public class GoogleFarmaciaRegistroEnSesionService
    {

        public static GoogleFarmacia getSessionGoogleFarmaciaRegistro(HttpContext context)
        {
            string cadenaGoogleFarmacia = context.Session.GetString("googleFarmaciaRegistro");

            GoogleFarmacia farmaciaRegistroEnSesion = JsonSerializer.Deserialize<GoogleFarmacia>(cadenaGoogleFarmacia);
            return farmaciaRegistroEnSesion;
        }

        public static void setSessionGoogleFarmaciaRegistro(HttpContext context, GoogleFarmacia farmacia)
        {
            string farmaciaRegistroEnSesion = JsonSerializer.Serialize(farmacia);
            context.Session.SetString("googleFarmaciaRegistro", farmaciaRegistroEnSesion);
        }

    }
}
