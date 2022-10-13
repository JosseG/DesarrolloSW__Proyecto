namespace LabPortugal_Intranet.Models
{
    public class Facturacion
    {

        private string id { get; set; }
        private string producto { get; set; }
        private DateTime fechaEmision { get; set; }

        private string rucFarmacia { get; set; }
        private string razonSocial { get; set; }
        private double subTotal { get; set; }
        private bool estado { get; set; }
    }
}
