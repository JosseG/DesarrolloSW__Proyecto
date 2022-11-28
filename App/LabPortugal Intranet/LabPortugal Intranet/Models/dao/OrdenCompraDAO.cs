using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class OrdenCompraDAO : ICrud<OrdenCompra>
    {
        public void Actualizar(OrdenCompra o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_orden_compra_actualizar @idordencompra,@idlaboratorio,@idproducto,@numordencompra,@fechaordencompra,@fechaentrega, @condicionespago, @valortotalorden", connection);

                    command.Parameters.AddWithValue("@idordencompra", o.id);
                    command.Parameters.AddWithValue("@idlaboratorio", o.idLaboratorio);
                    command.Parameters.AddWithValue("@idproducto", o.idProducto);
                    command.Parameters.AddWithValue("@numordencompra", o.numero);
                    command.Parameters.AddWithValue("@fechaordencompra", o.fechaPedido);
                    command.Parameters.AddWithValue("@fechaentrega", o.fechaEntrega);
                    command.Parameters.AddWithValue("@condicionespago", o.condicionesPago);
                    command.Parameters.AddWithValue("@valortotalorden", o.valor);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void Agregar(OrdenCompra o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {
                    SqlCommand command = new SqlCommand("exec usp_orden_compra_agregar @idordencompra,@idlaboratorio,@idproducto,@numordencompra,@fechaordencompra,@fechaentrega, @condicionespago, @valortotalorden", connection);

                    command.Parameters.AddWithValue("@idordencompra", o.id);
                    command.Parameters.AddWithValue("@idlaboratorio", o.idLaboratorio);
                    command.Parameters.AddWithValue("@idproducto", o.idProducto);
                    command.Parameters.AddWithValue("@numordencompra", o.numero);
                    command.Parameters.AddWithValue("@fechaordencompra", o.fechaPedido);
                    command.Parameters.AddWithValue("@fechaentrega", o.fechaEntrega);
                    command.Parameters.AddWithValue("@condicionespago", o.condicionesPago);
                    command.Parameters.AddWithValue("@valortotalorden", o.valor);

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
                    SqlCommand command = new SqlCommand("exec usp_orden_compra_eliminar @idordencompra", connection);

                    command.Parameters.AddWithValue("@idordencompra", o);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public List<OrdenCompra> ObtenerTodos()
        {
            var list = new List<OrdenCompra>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_orden_compra_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new OrdenCompra()
                        {
                            id = reader.GetInt32(0),
                            idLaboratorio = reader.GetString(1),
                            idProducto = reader.GetString(2),
                            numero = reader.GetString(3),
                            fechaPedido = reader.GetDateTime(4),
                            fechaEntrega = reader.GetDateTime(5),
                            condicionesPago = reader.GetString(6),
                            valor = reader.GetDouble(7),
                            estado = reader.GetBoolean(8)
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

        public OrdenCompra ObtenerXId(object o)
        {
            //Debug.WriteLine(o);
            if (o != null)
            {
                return ObtenerTodos().FirstOrDefault(f => f.id.Equals(o));
            }
            return new OrdenCompra();

        }
    }
}
