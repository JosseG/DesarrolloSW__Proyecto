using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class ProductoDAO : ICrud<Producto>
    {
        public void Actualizar(Producto o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_producto_actualizar @id, @idlaboratorio, @idtipo, @codigobarras, @descripcion, @marca, @stockproducto,@precioproducto", connection);
                    command.Parameters.AddWithValue("@id", o.id);
                    command.Parameters.AddWithValue("@idlaboratorio", o.idLaboratorio);
                    command.Parameters.AddWithValue("@idtipo", o.idTipoProducto);
                    command.Parameters.AddWithValue("@codigobarras", o.codigoBarra);
                    command.Parameters.AddWithValue("@descripcion", o.descripcion);
                    command.Parameters.AddWithValue("@marca", o.marca);
                    command.Parameters.AddWithValue("@stockproducto", o.stock);
                    command.Parameters.AddWithValue("@precioproducto", o.precioUnidad);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void Agregar(Producto o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_producto_agregar @id, @idlaboratorio, @idtipo, @codigobarras, @descripcion, @marca, @stockproducto,@precioproducto", connection);
                    command.Parameters.AddWithValue("@id", o.id);
                    command.Parameters.AddWithValue("@idlaboratorio", o.idLaboratorio);
                    command.Parameters.AddWithValue("@idtipo", o.idTipoProducto);
                    command.Parameters.AddWithValue("@codigobarras", o.codigoBarra);
                    command.Parameters.AddWithValue("@descripcion", o.descripcion);
                    command.Parameters.AddWithValue("@marca", o.marca);
                    command.Parameters.AddWithValue("@stockproducto", o.stock);
                    command.Parameters.AddWithValue("@precioproducto", o.precioUnidad);

                    command.ExecuteNonQuery();
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
                    SqlCommand command = new SqlCommand("exec usp_producto_eliminar @id", connection);
                    command.Parameters.AddWithValue("@id", o);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
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
            Debug.WriteLine(o);
            if (o != null)
            {
                return ObtenerTodos().FirstOrDefault(f => f.id.Equals(o));
            }
            return new Producto();
        }
    }
}
