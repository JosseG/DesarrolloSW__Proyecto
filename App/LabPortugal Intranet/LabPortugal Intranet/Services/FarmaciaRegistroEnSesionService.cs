using LabPortugal_Intranet.Models;
using System.Text.Json;

namespace LabPortugal_Intranet.Services
{
    public class FarmaciaRegistroEnSesionService
    {

        public static Farmacia getSessionFarmaciaRegistro(HttpContext context)
        {
            String cadenaFarmacia = context.Session.GetString("farmaciaRegistro");

            Farmacia farmaciaRegistroEnSesion = JsonSerializer.Deserialize<Farmacia>(cadenaFarmacia);
            return farmaciaRegistroEnSesion;
        }

        public static void setSessionFarmaciaRegistro(HttpContext context, Farmacia farmacia)
        {
            string farmaciaRegistroEnSesion = JsonSerializer.Serialize(farmacia);
            context.Session.SetString("farmaciaRegistro", farmaciaRegistroEnSesion);
        }

    }
}
