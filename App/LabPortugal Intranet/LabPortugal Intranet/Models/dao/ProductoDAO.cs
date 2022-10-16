using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;

namespace LabPortugal_Intranet.Models.dao
{
    public class ProductoDAO : ICrud<Producto>
    {
        public void Actualizar(Producto o)
        {
            throw new NotImplementedException();
        }

        public void Agregar(Producto o)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(object o)
        {
            throw new NotImplementedException();
        }

        public List<Producto> ObtenerTodos()
        {
            var list = new List<Producto>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_productos_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        list.Add(new Producto()
                        {
                            id = reader.GetString(0),
                            idLaboratorio = reader.GetString(1),
                            idTipoProducto = reader.GetInt32(2),
                            codigoBarra = reader.GetString(3),
                            descripcion = reader.GetString(4),
                            marca = reader.GetString(5),
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

        public Producto ObtenerXId(object o)
        {
            throw new NotImplementedException();
        }
    }
}
