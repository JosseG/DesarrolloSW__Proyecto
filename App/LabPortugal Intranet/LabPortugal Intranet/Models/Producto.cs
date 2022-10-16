namespace LabPortugal_Intranet.Models
{
    public class Producto
    {
        public string id { get; set; }
        public string idLaboratorio { get; set; }
        public int idTipoProducto { get; set; }
        public string codigoBarra { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public bool estado { get; set; }


    }
}
