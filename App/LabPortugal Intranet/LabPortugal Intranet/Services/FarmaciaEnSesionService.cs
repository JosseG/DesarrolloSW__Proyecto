using LabPortugal_Intranet.Models;
using System.Text.Json;

namespace LabPortugal_Intranet.Services
{
    public class FarmaciaEnSesionService
    {

        public static Farmacia getSessionFarmacia(HttpContext context)
        {
            String cadenaFarmacia = context.Session.GetString("farmacia");

            if(cadenaFarmacia == null)
            {
                return null ;
            }

            Farmacia farmaciaEnSesion = JsonSerializer.Deserialize<Farmacia>(cadenaFarmacia);
            return farmaciaEnSesion;
        }

        public static void setSessionFarmacia(HttpContext context, Farmacia farmacia)
        {
            string farmaciaEnSesion = JsonSerializer.Serialize(farmacia);
            context.Session.SetString("farmacia", farmaciaEnSesion);
        }

    }
}
