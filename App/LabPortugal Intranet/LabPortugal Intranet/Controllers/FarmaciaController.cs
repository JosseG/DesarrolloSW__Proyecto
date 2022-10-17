using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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


        public ActionResult Detalles(string id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }


        public ActionResult Eliminar(string id)
        {
            farmaciadao.Eliminar(id);
            return RedirectToAction("Index");

        }

        
        public ActionResult Actualizar(string id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }


        [HttpPost]
        public ActionResult Actualizar(Farmacia farmacia)
        {
            farmaciadao.Actualizar(farmacia);
            return View(farmacia);
        }




    }
}
