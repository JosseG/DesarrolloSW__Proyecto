using System.ComponentModel.DataAnnotations;

namespace LabPortugal_Intranet.Models
{
    public class FacturacionModel
    {

        [Display(Name = "Producto")] public String item { get; set; }
        [Display(Name = "Fecha de emisión")] public String fechaEmision { get; set; }
        [Display(Name = "Ruc de Farmacia")] public String rucFarmacia { get; set; }
        [Display(Name = "Razon Social")] public String razonSocial { get; set; }
        [Display(Name = "SubTotal")] public Double subTotal { get; set; }

    }
}
