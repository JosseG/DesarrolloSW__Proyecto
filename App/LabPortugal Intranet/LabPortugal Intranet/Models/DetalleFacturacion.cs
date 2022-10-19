namespace LabPortugal_Intranet.Models
{
    public class DetalleFacturacion
    {

        public string idFacturacion { get; set; }
        public string idProducto { get; set; }
        public int cantidadProducto { get; set; }
        public float monto { get; set; }
        public bool estado { get; set; }

    }
}
