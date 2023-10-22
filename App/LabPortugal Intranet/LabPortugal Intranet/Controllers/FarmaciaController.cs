using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;
namespace LabPortugal_Intranet.Controllers
{
    [Authorize]
    public class FarmaciaController : Controller
    {
        private readonly IHostingEnvironment _env;

        
        LaboratorioDAO laboratorioDAO = new LaboratorioDAO();
        TipoProductoDAO tipoproductodao = new TipoProductoDAO();
        ProductoDAO productodao = new ProductoDAO();
        FarmaciaDAO farmaciadao = new FarmaciaDAO();
        //Nuevo DAO

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        //##################################################################################
        //-------------------MANTENIMIENTO FARMACIA------------------------
        //##################################################################################
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
                return RedirectToAction("MantenimientoFarmacia");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }
        public ActionResult Eliminar(string id)
        {
            farmaciadao.Eliminar(id);
            return RedirectToAction("MantenimientoFarmacia");
        }
        public ActionResult Actualizar(string id)
        {
            if (id == null)
                return RedirectToAction("MantenimientoFarmacia");

            Farmacia farmacia = farmaciadao.ObtenerXId(id.ToString());
            return View(farmacia);
        }
        [HttpPost]
        public ActionResult Actualizar(Farmacia farmacia)
        {
            farmaciadao.Actualizar(farmacia);
            return View(farmacia);
        }


        //###################################################################################
        //--------------------------MANTENIMIENTO STOCK PRODUCTO---------------------------
        //###################################################################################
        public ActionResult MantenimientoStockProducto(string nombre, string id)
        {
            if (nombre == null) { nombre = string.Empty; }
            if (id == null) { id = string.Empty; }
            ViewBag.nombre = nombre;
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre", id);
            var lista = from p in productodao.ObtenerTodos()
                        where (p.descripcion.ToUpper().Contains(nombre.ToUpper()))
                        && (id != "" && p.idTipoProducto == Convert.ToInt32(id))
                        select p;
            return View(lista.ToList());

        }
        public ActionResult AgregarStockProducto()
        {
            
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre");
            return View(new Producto());
        }
        [HttpPost]
        public ActionResult AgregarStockProducto(Producto producto, IFormFile postedFile)
        {          
            producto.imagenProducto = Path.GetFileName(postedFile.FileName);
            productodao.Agregar(producto);

            string ruta = Path.Combine(_env.WebRootPath, "producto", producto.imagenProducto);           
            using (FileStream stream = new FileStream(ruta, FileMode.Create))
            {
                postedFile.CopyTo(stream);  
            }
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre");
                var idTipoProducto = producto.idTipoProducto;
                return View(producto);
            
        }
        public ActionResult ActualizarStockProducto(string id)
        {
            if (id == null)
                // ReSharper disable once Mvc.ActionNotResolved
                return RedirectToAction("ObtenerTodos");
            Producto producto = productodao.ObtenerXId(id.ToString());
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre", producto.idTipoProducto);
            return View(producto);

        }
        [HttpPost]
        public ActionResult ActualizarStockProducto(Producto producto)
        {
            productodao.Actualizar(producto);
            return View(producto);
        }

        public ActionResult DetalleStockProducto(string id)
        {
            if (id == null)
                // ReSharper disable once Mvc.ActionNotResolved
                return RedirectToAction("ObtenerTodos");
            Producto producto = productodao.ObtenerXId(id.ToString());
            return View(producto);
        }

        public ActionResult EliminarStockProducto(string id)
        {
            if (id == null)
                // ReSharper disable once Mvc.ActionNotResolved
                return RedirectToAction("ObtenerTodos");
            Producto producto = productodao.ObtenerXId(id.ToString());
            productodao.Eliminar(id);
            return View(producto);
        }



        //##################################################################################
        //---------------------------------VENTA PRODUCTO----
        //##################################################################################

        public ActionResult VentaProducto(string nombre, string id)
        {
            if (nombre == null) { nombre = string.Empty; }
            if (id == null) { id = string.Empty; }
            ViewBag.nombre = nombre;
            ViewBag.TipoProducto = new SelectList(tipoproductodao.ObtenerTodos().ToList(), "id", "nombre", id);
            var lista = from p in productodao.ObtenerTodos()
                        where p.descripcion.ToUpper().Contains(nombre.ToUpper())
                        && (id != "" && p.idTipoProducto == Convert.ToInt32(id))
                        select p;
            return View(lista.ToList());
        }
        public ActionResult DetalleVentaProducto(string id)
        {
            if (id == null)
                // ReSharper disable once Mvc.ActionNotResolved
                return RedirectToAction("ObtenerTodos");
            Producto producto = productodao.ObtenerXId(id.ToString());
            return View(producto);
        }
        public ActionResult ListaOrdenCompra(string id)
        {
            if (id == null)
                // ReSharper disable once Mvc.ActionNotResolved
                return RedirectToAction("ObtenerTodos");
            Producto producto = productodao.ObtenerXId(id.ToString());
            // ReSharper disable once Mvc.ViewNotResolved
            return View(producto);
            //return View(ordenCompraDAO.ObtenerTodos());
        }



        public FarmaciaController(IHostingEnvironment env)
        {
            _env = env;
        }
        public ActionResult Imagen(string id)
        {
            Producto producto = productodao.ObtenerXId(id);
            MemoryStream memoryStream;
            if (!String.IsNullOrEmpty(producto.imagenProducto))
            {
                string path = Path.Combine(_env.WebRootPath, "producto", producto.imagenProducto);
                //string path = HttpContext. Current.Server.MapPath(producto.imagenProducto);
                byte[] byteimage = System.IO.File.ReadAllBytes(path);
                memoryStream = new MemoryStream(byteimage);
                Image image = Image.FromStream(memoryStream);
                memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                memoryStream.Position = 0;
                
            }
            else
                 memoryStream = new MemoryStream();

            return File(memoryStream, "image/Jpg");
        }

    }
}
