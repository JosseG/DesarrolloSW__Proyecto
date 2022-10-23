namespace LabPortugal_Intranet.Models
{
    public class OrdenCompra
    {

        public int id { get; set; }
        public string idLaboratorio { get; set; }
        public string idProducto { get; set; }
        public string numero { get; set; }
        public DateTime fechaPedido { get; set; }
        public DateTime fechaEntrega { get; set; }
        public string condicionesPago { get; set; }
        public double valor { get; set; }
        public bool estado { get; set; }

    }
}
