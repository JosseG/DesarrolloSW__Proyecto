using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class FarmaciaDAO : ICrud<Farmacia>
    {
        public void Actualizar(Farmacia o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_farmacia_actualizar @id,@ruc,@razonsocial,@telefono,@direccion", connection);

                    command.Parameters.AddWithValue("@id", o.id);
                    command.Parameters.AddWithValue("@ruc", o.ruc);
                    command.Parameters.AddWithValue("@razonsocial", o.razonSocial);
                    command.Parameters.AddWithValue("@telefono", o.telefono);
                    command.Parameters.AddWithValue("@direccion", o.direccion);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }



        public void Agregar(Farmacia o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_farmacia_agregar @id,@ruc,@razonsocial,@telefono,@direccion,@hasgoogleaccount", connection);

                    command.Parameters.AddWithValue("@id", o.id);
                    command.Parameters.AddWithValue("@ruc", o.ruc);
                    command.Parameters.AddWithValue("@razonsocial", o.razonSocial);
                    command.Parameters.AddWithValue("@telefono", o.telefono);
                    command.Parameters.AddWithValue("@direccion", o.direccion);
                    command.Parameters.AddWithValue("@hasgoogleaccount", o.hasGoogleAccount);

                    command.ExecuteNonQuery();
                    Debug.WriteLine("LLego a agregar farmacia");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
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

                    SqlCommand command = new SqlCommand("exec usp_farmacia_eliminar @id", connection);

                    command.Parameters.AddWithValue("@id", o);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void ActualizarEstado(object o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_farmacia_actualizado_estado @id", connection);

                    command.Parameters.AddWithValue("@id", o);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public List<Farmacia> ObtenerTodos()
        {
            var list = new List<Farmacia>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_farmacias_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        list.Add(new Farmacia()
                        {
                            id = reader.GetString(0),
                            ruc = reader.GetString(1),
                            razonSocial = reader.GetString(2),
                            telefono = reader.GetString(3),
                            direccion = reader.GetString(4),
                            hasGoogleAccount = reader.GetBoolean(5),
                            estado = reader.GetBoolean(6)
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return list;

                /*using(SqlCommand command = new SqlCommand("exec usp_productos_listar", connection))
                {

                }*/
            }
        }

        
        public Farmacia ObtenerXId(object o)
        {
            Debug.WriteLine("Codigo enviado de farmacia " + o);
            if (o != null)
            {
                return ObtenerTodos().FirstOrDefault(f => f.id.Equals(o));
            }
            return new Farmacia();
            
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

                    SqlCommand command = new SqlCommand("select dbo.autogenerafarmacia()", connection);

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
    }
}
