namespace LabPortugal_Intranet.Services
{
    public class SunatApiService
    {


        public string getUrlValueSunatApi(IConfiguration configuration, string ruc)
        {

            var url = "https://api.apis.net.pe/v2/sunat/ruc?numero=" + ruc;

            return url;
        }


    }
}
