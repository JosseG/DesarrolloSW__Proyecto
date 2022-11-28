using LabPortugal_Intranet.Commons.Imp;
using System.Data;
using System.Data.SqlClient;

namespace LabPortugal_Intranet.Services
{
    public class FacturacionService
    {

        public DataTable obtenerUltimaFactura()
        {
            var dt = new DataTable();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec usp_facturacion_farmacia_producto_ultimo_report", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }

    }
}
