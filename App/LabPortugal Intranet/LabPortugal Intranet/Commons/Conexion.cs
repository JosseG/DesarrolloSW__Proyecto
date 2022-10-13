using System.Data.SqlClient;

namespace LabPortugal_Intranet.Commons
{
    public class Conexion
    {

        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        public SqlConnection getConnection()
        {
            return connection;
        }

    }
}
