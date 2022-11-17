using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace LabPortugal_Intranet.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        OrdenCompra ordcom = new Models.OrdenCompra();
        DetalleFacturacion detfac = new Models.DetalleFacturacion();
        Producto prod = new Models.Producto();

        //Dao
        ProductoDAO productoDao = new ProductoDAO();

        //carrito item
        CarritoItem carritoItem = new CarritoItem();
        public IActionResult Index(String id)
        {
            return View();
        }


        // En el del ejemplo es "int" en vez de string el "id"
        private int getIndice(string id)
        {
            var str = HttpContext.Session.GetString("carrito");
            List<CarritoItem> carritoItems = JsonSerializer.Deserialize<List<CarritoItem>>(str);
            for (int i = 0; i < carritoItems.Count; i++)
            {
                if (carritoItems[i].Producto.id == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public ActionResult agregarCarrito()
        {
            return View();
        }

        [HttpPost]
        public ActionResult agregarCarrito(string id, int cantidad)
        {
            id = id.Replace("\n", "").Trim();
            var str = HttpContext.Session.GetString("carrito");
            if (str == null)
            {

                List<CarritoItem> carritoItems = new List<CarritoItem>();
               
                carritoItems.Add(new CarritoItem(productoDao.ObtenerXId(id), cantidad));
                var contenido = JsonSerializer.Serialize(carritoItems);
                HttpContext.Session.SetString("carrito", contenido);
            }
            else
            {
                List<CarritoItem> carritoItems = JsonSerializer.Deserialize<List<CarritoItem>>(str);
                int indice = getIndice(id);
                if (indice == -1)
                {
                    carritoItems.Add(new CarritoItem(productoDao.ObtenerXId(id), cantidad));
                }
                else
                {
                    carritoItems[indice].Cantidad += cantidad;
                }

                var content = JsonSerializer.Serialize(carritoItems);
                HttpContext.Session.SetString("carrito", content);
            }
            return Json(new { response = true });
        }

        
        public ActionResult Delete(string id)
        {
            var str = HttpContext.Session.GetString("carrito");
            List<CarritoItem> carritoItems = JsonSerializer.Deserialize<List<CarritoItem>>(str);
            carritoItems.RemoveAt(getIndice(id));
            Debug.WriteLine(carritoItems.Count);
            var content = JsonSerializer.Serialize(carritoItems);
            HttpContext.Session.SetString("carrito", content);
            return View("agregarCarrito");
        }

        /*public ActionResult FinalizarCompra()
        {
            return View();
        }*/

    }
}
