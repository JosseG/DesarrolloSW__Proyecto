using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;

namespace LabPortugal_Intranet.Models.dao
{
    public class FarmaciaDAO : ICrud<Farmacia>
    {
        public void Actualizar(Farmacia o)
        {
            throw new NotImplementedException();
        }

        public void Agregar(Farmacia o)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(object o)
        {
            throw new NotImplementedException();
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
                            estado = reader.GetBoolean(5)
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
            throw new NotImplementedException();
        }
    }
}
