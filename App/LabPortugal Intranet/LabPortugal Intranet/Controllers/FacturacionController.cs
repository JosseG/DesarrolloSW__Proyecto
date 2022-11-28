using AspNetCore.Reporting;
using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Models.dao;
using LabPortugal_Intranet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace LabPortugal_Intranet.Controllers
{
    public class FacturacionController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        FacturacionDAO facturacionDAO = new FacturacionDAO();
        ProductoDAO productoDAO = new ProductoDAO();
        DetalleFacturacionDAO detalleFacturacionDAO = new DetalleFacturacionDAO();

        FacturacionService facturacionService = new FacturacionService();

        public FacturacionController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        }

        public IActionResult IndexarDataFacturacion()
        {


            return RedirectToAction("AgregarFacturacion");
        }

        public IActionResult Index()
        {
            return View(facturacionDAO.ObtenerTodos());
        }



        
        public ActionResult AgregarFacturacion()
        {
            String carritoSession = HttpContext.Session.GetString("carrito");

            List<CarritoItem> listaItems = JsonSerializer.Deserialize<List<CarritoItem>>(carritoSession);
            List<FacturacionModel> listaFacturacionModel = new List<FacturacionModel>();

            Producto producto = new Producto();

            string codigo = facturacionDAO.ObtenerIdCorrelativo();
            Debug.WriteLine(codigo);
            List<DetalleFacturacion> listaDetalle = new List<DetalleFacturacion>();
            float subtotal = 0f;

            if (listaItems.Count() > 0)
            {
                
                foreach (CarritoItem en in listaItems)
                {
                    DetalleFacturacion detalleFacturacion = new DetalleFacturacion();
                    detalleFacturacion.idFacturacion = codigo;
                   
                    detalleFacturacion.idProducto = en.Producto.id;
                    detalleFacturacion.cantidadProducto = en.Cantidad;
                    detalleFacturacion.monto = ((float)(en.Producto.precioUnidad * en.Cantidad));
                    listaDetalle.Add(detalleFacturacion);
                    subtotal += detalleFacturacion.monto;
                    
                }
                Debug.WriteLine(FarmaciaEnSesionService.getSessionFarmacia(HttpContext).id);
                Facturacion facturacion = new Facturacion();
                facturacion.id = codigo;
                facturacion.idFarmacia = FarmaciaEnSesionService.getSessionFarmacia(HttpContext).id;
                facturacion.fechaEmision = DateTime.Now;
                facturacion.subTotal = subtotal;

                Debug.WriteLine(facturacion.fechaEmision);
                facturacionDAO.Agregar(facturacion);

                foreach (DetalleFacturacion det in listaDetalle)
                {
                    detalleFacturacionDAO.Agregar(det);
                    Debug.WriteLine(det.idFacturacion + " " + det.idProducto + " " + det.cantidadProducto);

                    Producto productoObtenido = productoDAO.ObtenerXId(det.idProducto);
                    productoObtenido.stock = productoObtenido.stock - det.cantidadProducto;

                    productoDAO.Actualizar(productoObtenido);

                    Debug.WriteLine("producto actualizado");
                }

 
            }


            var content = JsonSerializer.Serialize(new List<CarritoItem>());
            HttpContext.Session.SetString("carrito", content);
            return View(facturacionDAO.obtenerFacturacionModelUltimo());


        }


        public IActionResult FacturaReportInfo()
        {

            var tablaDatos = new DataTable();

            tablaDatos = facturacionService.obtenerUltimaFactura();


            string mimetype = "";
            int extension = 1;
            var path = $"{this._hostingEnvironment.WebRootPath}\\reports\\Factura.rdlc";


            LocalReport localReport  = new LocalReport(path);
            localReport.AddDataSource("MiReporteDataSet", tablaDatos);
            Dictionary<String, String> parametros = new Dictionary<string, string>();
            parametros.Add("NombreFarmacia",FarmaciaEnSesionService.getSessionFarmacia(HttpContext).razonSocial);
            parametros.Add("RucFarmacia", FarmaciaEnSesionService.getSessionFarmacia(HttpContext).ruc);
            parametros.Add("FonoFarmacia", FarmaciaEnSesionService.getSessionFarmacia(HttpContext).telefono);
            parametros.Add("LaboratorioRUC", "R1379477414");


            var result = localReport.Execute(RenderType.Pdf,extension, parametros, mimetype);

            return File(result.MainStream, "application/pdf");


        }


    }
}
