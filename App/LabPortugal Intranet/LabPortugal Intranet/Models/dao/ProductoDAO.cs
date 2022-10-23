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
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_producto_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Producto()
                        {
                            id = reader.GetString(0),
                            idLaboratorio = reader.GetString(1),
                            idTipoProducto = reader.GetInt32(2),
                            TipoProducto = reader.GetString(3),
                            codigoBarra = reader.GetString(4),
                            descripcion = reader.GetString(5),
                            marca = reader.GetString(6),
                            stock = reader.GetInt32(7),
                            precioUnidad = reader.GetDouble(8),
                            estado = reader.GetBoolean(9)

                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return list;
            }
        }

        public Producto ObtenerXId(object o)
        {
            throw new NotImplementedException();
        }
    }
}
