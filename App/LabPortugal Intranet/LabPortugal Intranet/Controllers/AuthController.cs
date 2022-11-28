using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using LabPortugal_Intranet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LabPortugal_Intranet.Controllers
{
    public class AuthController : Controller
    {

        UsuarioFarmaciaDAO usuarioFarmaciaDAO = new UsuarioFarmaciaDAO();
        GoogleFarmaciaDAO googleFarmaciaDAO = new GoogleFarmaciaDAO();
        CargoUsuarioFarmaciaDAO cargoUsuarioFarmaciaDAO = new CargoUsuarioFarmaciaDAO();
        CargoUsuarioGoogleDAO cargoUsuarioGoogleDAO = new CargoUsuarioGoogleDAO();
        CargoDAO cargoDAO = new CargoDAO();
        FarmaciaDAO farmaciaDAO = new FarmaciaDAO();


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UsuarioFarmacia usuarioFarmacia)
        {
            Debug.WriteLine(usuarioFarmacia.alias);
            Debug.WriteLine(usuarioFarmacia.contrasena);
            UsuarioFarmacia usuarioFarmaciaObtenido = usuarioFarmaciaDAO.validarUsuario(usuarioFarmacia.alias, usuarioFarmacia.contrasena);
            if (usuarioFarmaciaObtenido != null)
            {
                List<CargoUsuarioFarmacia> cargoUsuarioFarmacias = cargoUsuarioFarmaciaDAO.ObtenerTodosCargoXUsuarioFarmacia(usuarioFarmaciaObtenido.id);

                Farmacia farmacia = farmaciaDAO.ObtenerXId(usuarioFarmaciaObtenido.idFarmacia);

                Debug.WriteLine(farmacia.id + " - " + farmacia.razonSocial + " - " + farmacia.estado);

                if (farmacia.estado == false)
                {
                    return RedirectToAction("Index");
                }

                FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmacia);
                UsuarioEnSesionService.setSessionUser(HttpContext, usuarioFarmaciaObtenido);



                var claims = new List<Claim>();


                claims.Add(new Claim(ClaimTypes.Name, farmacia.razonSocial));
                claims.Add(new Claim("Correo", usuarioFarmaciaObtenido.alias));

                foreach (CargoUsuarioFarmacia rol in cargoUsuarioFarmacias)
                {
                    var element = cargoDAO.ObtenerXId(rol.idCargo);
                    claims.Add(new Claim(ClaimTypes.Role, element.rol));
                }


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index", "Farmacia", new { area = "" });
            }
            return RedirectToAction("Index");
        }

        public async Task LogInWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
            
        }

        public async Task SignUpWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string authIdentifierUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Debug.WriteLine(authIdentifierUser);

            GoogleFarmacia googleFarmacia = googleFarmaciaDAO.ObtenerXId(authIdentifierUser);
            Debug.WriteLine(googleFarmacia==null?"Es null google farmacia":"Todo bien");

            if (googleFarmacia == null)
            {
                Farmacia farmacia = new Farmacia();
                farmacia.id = farmaciaDAO.ObtenerIdCorrelativo();
                farmacia.hasGoogleAccount = true;
                FarmaciaRegistroEnSesionService.setSessionFarmaciaRegistro(HttpContext, farmacia);
                return RedirectToAction("SignUpPharmacy");
            }
            Farmacia farmaciaToSesion = farmaciaDAO.ObtenerXId(googleFarmacia.idFarmacia);

            Debug.WriteLine(farmaciaToSesion.id + " - " + farmaciaToSesion.razonSocial + " - " + farmaciaToSesion.estado);

            if (farmaciaToSesion.estado == false)
            {
                return RedirectToAction("Index");
            }
            FarmaciaEnSesionService.setSessionFarmacia(HttpContext, farmaciaToSesion);
            GoogleFarmaciaEnSesionService.setSessionGoogleFarmacia(HttpContext, googleFarmacia);

            List<CargoUsuarioGoogle> cargosUsuarioGoogle = cargoUsuarioGoogleDAO.ObtenerTodosCargoXUsuarioGoogle(googleFarmacia.idGoogleAuth);


            var claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.Name, farmaciaToSesion.razonSocial));
            claims.Add(new Claim("Correo", googleFarmacia.idGoogleAuth));

            foreach (CargoUsuarioGoogle rol in cargosUsuarioGoogle)
            {
                var element = cargoDAO.ObtenerXId(rol.idCargo);
                claims.Add(new Claim(ClaimTypes.Role, element.rol));
            }


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Farmacia", new { area = "" });
        }

        public IActionResult SignUp()
        {
            FarmaciaRegistroEnSesionService.setSessionFarmaciaRegistro(HttpContext, null);
            return RedirectToAction("SignUpPharmacy");
        }

        public IActionResult SignUpPharmacy()
        {
            Farmacia farmacia = FarmaciaRegistroEnSesionService.getSessionFarmaciaRegistro(HttpContext);
            if (farmacia != null)
            {
                Debug.WriteLine(farmacia.hasGoogleAccount);
                return View(farmacia);
            }
            farmacia = new Farmacia();
            farmacia.id = farmaciaDAO.ObtenerIdCorrelativo();
            return View(farmacia);
        }

        [HttpPost]
        public IActionResult SignUpPharmacy(Farmacia farmacia)
        {
            Debug.WriteLine(farmacia.id + " " + farmacia.hasGoogleAccount);

            //farmaciaDAO.Agregar(farmacia);
            if (farmacia.hasGoogleAccount)
            {
                GoogleFarmacia googleFarmacia = new GoogleFarmacia(); 
                googleFarmacia.idFarmacia = farmacia.id;
                googleFarmacia.idGoogleAuth = User.FindFirstValue(ClaimTypes.NameIdentifier);
                GoogleFarmaciaRegistroEnSesionService.setSessionGoogleFarmaciaRegistro(HttpContext, googleFarmacia);
                FarmaciaRegistroEnSesionService.setSessionFarmaciaRegistro(HttpContext, farmacia);
                return RedirectToAction("VerifyRuc", "Sunat");
            }
            else
            {
                UsuarioFarmacia usuarioFarmacia = new UsuarioFarmacia();
                FarmaciaRegistroEnSesionService.setSessionFarmaciaRegistro(HttpContext, farmacia);
                usuarioFarmacia.idFarmacia = farmacia.id;
                usuarioFarmacia.id = usuarioFarmaciaDAO.ObtenerIdCorrelativo();
                UsuarioEnSesionService.setSessionUser(HttpContext, usuarioFarmacia);
                return RedirectToAction("SignUpUserPharmacy");
            }

        }


        public IActionResult SignUpUserPharmacy()
        {
            UsuarioFarmacia usuarioFarmacia = UsuarioEnSesionService.getSessionUser(HttpContext);
            if(usuarioFarmacia != null)
            {
                return View(usuarioFarmacia);
            }
            return View(new UsuarioFarmacia());
        }

        [HttpPost]
        public IActionResult SignUpUserPharmacy(UsuarioFarmacia usuarioFarmacia)
        {
            UsuarioEnSesionService.setSessionUser(HttpContext, usuarioFarmacia);
            return RedirectToAction("VerifyRuc", "Sunat");
        }


        public void ToSignUp()
        {

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }


    }
}
