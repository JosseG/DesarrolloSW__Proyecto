namespace LabProtugal_Intranet.Models
{
    public class OrdenCompra
    {

        private int id { get; set; }
        private string idLaboratorio { get; set; }
        private string numero { get; set; }
        private DateTime fechaPedido { get; set; }
        private DateTime fechaEntrega { get; set; }
        private string condicionesPago { get; set; }
        private string transporte { get; set; }
        private double valor { get; set; }

    }
}
