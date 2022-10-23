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
              using (HttpClient client = new HttpClient())
                {
                  using (HttpResponseMessage res = await client.GetAsync(urlRequest))
                    {
                      using (HttpContent content = res.Content)
                        {
                           var data = await content.ReadAsStringAsync();
                            
                           if (data != null)
                            {
                                if (!data.Contains("<html>"))
                                {
                                    
                                    ViewBag.mensaje = data;
                                }
                                else
                                {
                                    ViewBag.mensaje = "RUC insertado fuera de formato";
                                }
                            }
                            else
                            {
                                ViewBag.mensaje = "Sin datos";

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            
            return View();
        }
        


    }
}
