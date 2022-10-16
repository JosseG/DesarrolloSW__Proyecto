using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;

namespace LabPortugal_Intranet.Controllers
{
    public class FarmaciaController : Controller
    {
        ProductoDAO productodao = new ProductoDAO();
        FarmaciaDAO farmaciadao = new FarmaciaDAO();

        public IActionResult Index()
        {
            return View(farmaciadao.ObtenerTodos());
        }



    }
}
