using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using LabPortugal_Intranet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabPortugal_Intranet.Controllers
{
    public class SunatController : Controller
    {
        FarmaciaDAO farmaciaDAO = new FarmaciaDAO();
        GoogleFarmaciaDAO googleFarmaciaDAO = new GoogleFarmaciaDAO();
        UsuarioFarmaciaDAO usuarioFarmaciaDAOimp = new UsuarioFarmaciaDAO();
        CargoUsuarioFarmaciaDAO cargoUsuarioFarmaciaDAO = new CargoUsuarioFarmaciaDAO();
        CargoUsuarioGoogleDAO cargoUsuarioGoogleDAO = new CargoUsuarioGoogleDAO();

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
            Farmacia farmacia = FarmaciaRegistroEnSesionService.getSessionFarmaciaRegistro(HttpContext);
            if(farmacia != null)
            {
                return View(farmacia);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> VerifyRuc(Farmacia farmacia)
        {
            Debug.WriteLine(farmacia.razonSocial);

            var urlRequest = new SunatApiService().getUrlValueSunatApi(_configuration, farmacia.ruc);

            try
            {
              using (HttpClient client = new HttpClient())
                {
                    var key = _configuration.GetValue<string>("Sunat:key");
                    /* var headerRequest = client.DefaultRequestHeaders;

                    headerRequest.Add("Accept", "application/json");
                    headerRequest.Add("Content-Type", "application/json");
                    headerRequest.Add("Authorization", $"Bearer {key}");*/
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlRequest);
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Authorization", $"Bearer {key}");
                    using (HttpResponseMessage res = await client.SendAsync(request))
                    {
                        Debug.WriteLine("Intenta validar");
                        if (res.IsSuccessStatusCode)
                        {
                            Debug.WriteLine("Validó todo");
                            Farmacia farmaciaSesion = FarmaciaRegistroEnSesionService.getSessionFarmaciaRegistro(HttpContext);
                            farmaciaSesion.ruc = farmacia.ruc;
                            Debug.WriteLine(farmacia.ruc);
                            if (farmaciaSesion.hasGoogleAccount)
                            {
                                GoogleFarmacia googleFarmacia = GoogleFarmaciaRegistroEnSesionService.getSessionGoogleFarmaciaRegistro(HttpContext);

                                Debug.WriteLine("Farmacia " + farmaciaSesion.id + " " + farmaciaSesion.direccion + " " + farmaciaSesion.ruc + " " + farmaciaSesion.razonSocial + " " + farmaciaSesion.hasGoogleAccount + " " + farmaciaSesion.estado);
                                Debug.WriteLine("Google Farmacia " + googleFarmacia.idFarmacia + " " + googleFarmacia.idGoogleAuth + " " + googleFarmacia.estado);

                                farmaciaDAO.Agregar(farmaciaSesion);
                                googleFarmaciaDAO.Agregar(googleFarmacia);

                                Debug.WriteLine("LLego a validar ruc");

                                CargoUsuarioGoogle cargoUsuarioGoogle = new CargoUsuarioGoogle();

                                cargoUsuarioGoogle.idCargo = 2;
                                cargoUsuarioGoogle.idGoogle = googleFarmacia.idGoogleAuth;

                                cargoUsuarioGoogleDAO.Agregar(cargoUsuarioGoogle);

                                FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmaciaSesion);
                                GoogleFarmaciaEnSesionService.setSessionGoogleFarmacia(HttpContext, googleFarmacia);
                            }
                            else
                            {
                                UsuarioFarmacia usuarioFarmacia = UsuarioEnSesionService.getSessionUser(HttpContext);


                                farmaciaDAO.Agregar(farmaciaSesion);
                                usuarioFarmaciaDAOimp.Agregar(usuarioFarmacia);


                                CargoUsuarioFarmacia cargoUsuarioFarmacia = new CargoUsuarioFarmacia();

                                cargoUsuarioFarmacia.idCargo = 2;
                                cargoUsuarioFarmacia.idFarmacia = usuarioFarmacia.id;

                                cargoUsuarioFarmaciaDAO.Agregar(cargoUsuarioFarmacia);


                                FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmaciaSesion);
                                UsuarioEnSesionService.setSessionUser(HttpContext, usuarioFarmacia);
                            }


                            return RedirectToAction("Index", "Auth");
                        }
                        else
                        {
                            ViewBag.mensaje = "RUC INVÁLIDO";
                        }
                        /*
                      using (HttpContent content = res.Content)
                        {
                           var data = await content.ReadAsStringAsync();
                            
                           if (data != null)
                            {
                                if (!data.Contains("<html>"))
                                {
                                    Farmacia farmaciaSesion = FarmaciaRegistroEnSesionService.getSessionFarmaciaRegistro(HttpContext);
                                    farmaciaSesion.ruc = farmacia.ruc;
                                    Debug.WriteLine(farmacia.ruc);
                                    if (farmaciaSesion.hasGoogleAccount)
                                    {
                                        GoogleFarmacia googleFarmacia = GoogleFarmaciaRegistroEnSesionService.getSessionGoogleFarmaciaRegistro(HttpContext);

                                        Debug.WriteLine("Farmacia " + farmaciaSesion.id + " " + farmaciaSesion.direccion + " " + farmaciaSesion.ruc + " " + farmaciaSesion.razonSocial + " " + farmaciaSesion.hasGoogleAccount + " " + farmaciaSesion.estado);
                                        Debug.WriteLine("Google Farmacia " + googleFarmacia.idFarmacia + " " + googleFarmacia.idGoogleAuth + " " + googleFarmacia.estado);

                                        farmaciaDAO.Agregar(farmaciaSesion);
                                        googleFarmaciaDAO.Agregar(googleFarmacia);

                                        Debug.WriteLine("LLego a validar ruc");

                                        CargoUsuarioGoogle cargoUsuarioGoogle = new CargoUsuarioGoogle();

                                        cargoUsuarioGoogle.idCargo = 2;
                                        cargoUsuarioGoogle.idGoogle = googleFarmacia.idGoogleAuth;

                                        cargoUsuarioGoogleDAO.Agregar(cargoUsuarioGoogle);

                                        FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmaciaSesion);
                                        GoogleFarmaciaEnSesionService.setSessionGoogleFarmacia(HttpContext, googleFarmacia);
                                    }
                                    else
                                    {
                                        UsuarioFarmacia usuarioFarmacia = UsuarioEnSesionService.getSessionUser(HttpContext);

                                        
                                        farmaciaDAO.Agregar(farmaciaSesion);
                                        usuarioFarmaciaDAOimp.Agregar(usuarioFarmacia);


                                        CargoUsuarioFarmacia cargoUsuarioFarmacia = new CargoUsuarioFarmacia();

                                        cargoUsuarioFarmacia.idCargo = 2;
                                        cargoUsuarioFarmacia.idFarmacia = usuarioFarmacia.id;

                                        cargoUsuarioFarmaciaDAO.Agregar(cargoUsuarioFarmacia);


                                        FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmaciaSesion);
                                        UsuarioEnSesionService.setSessionUser(HttpContext, usuarioFarmacia);
                                    }


                                    return RedirectToAction("Index","Auth");
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
                        
                        }*/
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
