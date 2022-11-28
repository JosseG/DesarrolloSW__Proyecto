namespace LabPortugal_Intranet.Models
{
    public class Facturacion
    {

        public string id { get; set; }

        public string idFarmacia { get; set; }
        public DateTime fechaEmision { get; set; }

        public float subTotal { get; set; }
        public bool estado { get; set; }
    }
}
