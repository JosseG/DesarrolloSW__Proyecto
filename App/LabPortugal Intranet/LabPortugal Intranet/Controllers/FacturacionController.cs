using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;

namespace LabPortugal_Intranet.Controllers
{
    public class FacturacionController : Controller
    {

        FacturacionDAO facturacionDAO = new FacturacionDAO();
        DetalleFacturacionDAO detalleFacturacionDAO = new DetalleFacturacionDAO();
        public IActionResult Index()
        {
            return View(facturacionDAO.ObtenerTodos());
        }

        public IActionResult Agregar()
        {
            return View(new Facturacion());
        }


        [HttpPost]
        public ActionResult Agregar(Facturacion facturacion)
        {
            facturacionDAO.Agregar(facturacion);
            return View(facturacion);
        }





    }
}
