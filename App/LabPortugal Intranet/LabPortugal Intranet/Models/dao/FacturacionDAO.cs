using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class FacturacionDAO : ICrud<Facturacion>
    {
        public void Actualizar(Facturacion o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_facturacion_actualizar @id,@idfarmacia,@fechaemision,@subtotal", connection);

                    command.Parameters.AddWithValue("@id", o.id);
                    command.Parameters.AddWithValue("@idfarmacia", o.idFarmacia);
                    command.Parameters.AddWithValue("@fechaemision", o.fechaEmision);
                    command.Parameters.AddWithValue("@subtotal", o.subTotal);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void Agregar(Facturacion o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_facturacion_agregar @idfarmacia,@fechaemision,@subtotal", connection);

                    command.Parameters.AddWithValue("@idfarmacia", o.idFarmacia);
                    command.Parameters.AddWithValue("@fechaemision", o.fechaEmision);
                    command.Parameters.AddWithValue("@subtotal", o.subTotal);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void Eliminar(object o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_facturacion_eliminar @id", connection);

                    command.Parameters.AddWithValue("@id", o);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public IEnumerable<FacturacionModel> obtenerFacturacionModelUltimo()
        {
            var list = new List<FacturacionModel>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_facturacion_farmacia_producto_ultimo", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Debug.WriteLine("Aqui si");
                        list.Add(new FacturacionModel()
                        {
                            item = reader.GetString(0),
                            fechaEmision = reader.GetDateTime(1).Date.ToString("dd/MM/yyyy"),
                            rucFarmacia = reader.GetString(2),
                            razonSocial = reader.GetString(3),
                            subTotal = reader.GetDouble(4)
                        });
                        Debug.WriteLine("Elsoa");
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                return list;

            }
        }
        public string ObtenerIdCorrelativo()
        {
            SqlConnection connection = new Conexion().getConnection();
            String idCorrelativo = "";
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("select dbo.autogenerafacturacion()", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        idCorrelativo = reader.GetString(0);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return idCorrelativo;
            }
        }

        public List<Facturacion> ObtenerTodos()
        {
            var list = new List<Facturacion>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_facturacion_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        list.Add(new Facturacion()
                        {
                            id = reader.GetString(0),
                            idFarmacia = reader.GetString(1),
                            fechaEmision = reader.GetDateTime(2),
                            subTotal = reader.GetFloat(3),
                            estado = reader.GetBoolean(4)
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return list;

            }
        }

        public Facturacion ObtenerXId(object o)
        {
            if (o != null)
            {
                return ObtenerTodos().FirstOrDefault(f => f.id.Equals(o));
            }
            return new Facturacion();
        }
    }
}
