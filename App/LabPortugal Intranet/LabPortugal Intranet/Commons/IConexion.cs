using System.Data.SqlClient;

namespace LabPortugal_Intranet.Commons
{
    public interface IConexion
    {
        SqlConnection getConnection();

    }
}
