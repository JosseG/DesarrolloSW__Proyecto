using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace LabPortugal_Intranet.Controllers
{
    public class AuthController : Controller
    {

        UsuarioFarmaciaDAO usuarioFarmaciaDAO = new UsuarioFarmaciaDAO();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn(UsuarioFarmacia usuarioFarmacia)
        {
            if(usuarioFarmaciaDAO.validarUsuario(usuarioFarmacia.alias, usuarioFarmacia.contrasena))
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

        public void SignUp()
        {

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

    }
}
