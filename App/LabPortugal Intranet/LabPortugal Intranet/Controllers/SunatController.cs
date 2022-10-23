using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabPortugal_Intranet.Controllers
{
    public class SunatController : Controller
    {
        private readonly IConfiguration _configuration;
        public SunatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult VerifyRuc()
        {
            return View(new Farmacia());
        }

        [HttpPost]
        public async Task<IActionResult> VerifyRuc(Farmacia farmacia)
        {

            var urlRequest = new SunatApiService().getUrlValueSunatApi(_configuration, farmacia.ruc);

            try
            {
                //We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    using (HttpResponseMessage res = await client.GetAsync(urlRequest))
                    {
                        //Then get the content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            //Now assign your content to your data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if (data != null)
                            {
                                //Now log your data in the console
                                Debug.WriteLine("data------------{0}"+data);
                                ViewBag.mensaje = data;
                            }
                            else
                            {
                                Debug.WriteLine("NO Data----------");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception Hit------------");
                Debug.WriteLine(exception);
            }
            
            return View();
        }
        


    }
}
