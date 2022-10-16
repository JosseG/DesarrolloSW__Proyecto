using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;

namespace LabPortugal_Intranet.Controllers
{
    public class FarmaciaController1 : Controller
    {

        ProductoDAO productodao = new ProductoDAO();

        public IActionResult Index()
        {
            return View(productodao.ObtenerTodos());
        }




    }
}
