namespace LabPortugal_Intranet.Services
{
    public class SunatApiService
    {


        public string getUrlValueSunatApi(IConfiguration configuration, string ruc)
        {
            var key = configuration.GetValue<string>("Sunat:key");
            var url = "https://dniruc.apisperu.com/api/v1/ruc/" + ruc + "?token=" + key;

            return url;
        }


    }
}
