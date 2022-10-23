using System.ComponentModel.DataAnnotations;

namespace LabPortugal_Intranet.Models
{
    public class Producto
    {
        [Display(Name = "Id")] public string id { get; set; }
        [Display(Name = "Id_Lab")] public string idLaboratorio { get; set; }
        [Display(Name = "idTipoProd")] public int idTipoProducto { get; set; }
        [Display(Name = "TipoProd")] public string TipoProducto { get; set; }
        [Display(Name = "CodBarra")] public string codigoBarra { get; set; }
        [Display(Name = "Descripcion")] public string descripcion { get; set; }
        [Display(Name = "Marca")] public string marca { get; set; }
        [Display(Name = "Stock")] public int stock { get; set; }
        [Display(Name = "PrecioUnid")] public double precioUnidad { get; set; }
        [Display(Name = "Estado")] public bool estado { get; set; }


    }
}
