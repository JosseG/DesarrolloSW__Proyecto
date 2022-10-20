using System.ComponentModel.DataAnnotations;

namespace LabPortugal_Intranet.Models
{
    public class Farmacia
    {
        [Display(Name = "Id")] public string id { get; set; }
        [Display(Name = "Ruc")] public string ruc { get; set; }
        [Display(Name = "Razón Social")] public string razonSocial { get; set; }
        [Display(Name = "Teléfono")] public string telefono { get; set; }
        [Display(Name = "Dirección")] public string direccion { get; set; }
        [Display(Name = "Estado")] public bool estado { get; set; }

    }
}
