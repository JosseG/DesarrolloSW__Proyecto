using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace LabPortugal_Intranet.Controllers
{
    public class FarmaciaController : Controller
    {
        TipoProductoDAO tipoproductodao = new TipoProductoDAO();
        ProductoDAO productodao = new ProductoDAO();
        FarmaciaDAO farmaciadao = new FarmaciaDAO();

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult MantenimientoFarmacia()
        {
            return View(farmaciadao.ObtenerTodos());
        }
        public ActionResult Agregar()
        {
            return View(new Farmacia());
        }

        [HttpPost]
        public ActionResult Agregar(Farmacia farmacia)
        {
            farmaciadao.Agregar(farmacia);
            return View(farmacia);
        }


        public ActionResult Detalles(string id)
        {
            if (id == null)
                return RedirectToAction("ObtenerTodos");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }


        public ActionResult Eliminar(string id)
        {
            farmaciadao.Eliminar(id);
            return RedirectToAction("ObtenerTodos");

        }

        
        public ActionResult Actualizar(string id)
        {
            if (id == null)
                return RedirectToAction("ObtenerTodos");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }

        [HttpPost]
        public ActionResult Actualizar(Farmacia farmacia)
        {
            farmaciadao.Actualizar(farmacia);
            return View(farmacia);
        }

        public ActionResult BuscarProductoTipoProducto(string nombre, string id)
        {
            if (nombre == null) { nombre = string.Empty; }
            if (id == null) { id = string.Empty; }
            ViewBag.nombre = nombre;
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre", id);
            var lista = from p in productodao.ObtenerTodos()
                        where p.descripcion.ToUpper().Contains(nombre.ToUpper())
                        && id != "" && p.idTipoProducto== Convert.ToInt32(id)
                        select p;
            return View(lista.ToList());

        }

    }
}
