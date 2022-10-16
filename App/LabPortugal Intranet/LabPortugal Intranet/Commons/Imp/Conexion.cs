using System.Data.SqlClient;

namespace LabPortugal_Intranet.Commons.Imp
{
    public class Conexion : IConexion
    {

        string cadena = string.Empty;
        private SqlConnection connection;


        public Conexion()
        {
            cadena = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetConnectionString("conexion");
            connection = new SqlConnection(cadena);
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

    }
}
