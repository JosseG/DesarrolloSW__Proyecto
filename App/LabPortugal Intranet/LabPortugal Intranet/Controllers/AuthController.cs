using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabPortugal_Intranet.Controllers
{
    public class AuthController : Controller
    {

        UsuarioFarmaciaDAO usuarioFarmaciaDAO = new UsuarioFarmaciaDAO();
        FarmaciaDAO farmaciaDAO = new FarmaciaDAO();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(UsuarioFarmacia usuarioFarmacia)
        {
            Debug.WriteLine(usuarioFarmacia.alias);
            Debug.WriteLine(usuarioFarmacia.contrasena);
            if (usuarioFarmaciaDAO.validarUsuario(usuarioFarmacia.alias, usuarioFarmacia.contrasena))
            {
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

        public async Task<IActionResult> GoogleResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Farmacia", new { area = "" });
        }

        public IActionResult SignUp()
        {
            return RedirectToAction("SignUpPharmacy");
        }

        public IActionResult SignUpPharmacy()
        {
            return View(new Farmacia());
        }

        [HttpPost]
        public IActionResult SignUpPharmacy(Farmacia farmacia)
        {
            //farmaciaDAO.Agregar(farmacia);


            return RedirectToAction("SignUpUserPharmacy");
        }


        public IActionResult SignUpUserPharmacy()
        {
            return View(new UsuarioFarmacia());
        }

        [HttpPost]
        public IActionResult SignUpUserPharmacy(UsuarioFarmacia usuarioFarmacia)
        {
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
