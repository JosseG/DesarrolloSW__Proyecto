using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class DetalleFacturacionDAO : ICrudExtension<DetalleFacturacion>
    {
        public void Actualizar(DetalleFacturacion o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_detalle_facturacion_actualizar @idfacturacion,@idproducto,@cantidadproducto,@monto", connection);

                    command.Parameters.AddWithValue("@idfacturacion", o.idFacturacion);
                    command.Parameters.AddWithValue("@idproducto", o.idProducto);
                    command.Parameters.AddWithValue("@cantidadproducto", o.cantidadProducto);
                    command.Parameters.AddWithValue("@monto", o.monto);

                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public void Agregar(DetalleFacturacion o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_detalle_facturacion_agregar @idfacturacion,@idproducto,@cantidadproducto,@monto", connection);

                    command.Parameters.AddWithValue("@idfacturacion", o.idFacturacion);
                    command.Parameters.AddWithValue("@idproducto", o.idProducto);
                    command.Parameters.AddWithValue("@cantidadproducto", o.cantidadProducto);
                    command.Parameters.AddWithValue("@monto", o.monto);

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
            throw new NotImplementedException();
        }

        public void Eliminar(object[] a)
        {

            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();

                try
                {

                    SqlCommand command = new SqlCommand("exec usp_detalle_facturacion_eliminar @idfacturacion,@idproducto", connection);

                    command.Parameters.AddWithValue("@idfacturacion", a[0]);
                    command.Parameters.AddWithValue("@idproducto", a[1]);
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            
        }

        public List<DetalleFacturacion> ObtenerTodos()
        {
            var list = new List<DetalleFacturacion>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_detalle_facturacion_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        list.Add(new DetalleFacturacion()
                        {
                            idFacturacion = reader.GetString(0),
                            idProducto = reader.GetString(1),
                            cantidadProducto = reader.GetInt32(2),
                            monto = reader.GetFloat(3),
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

        public DetalleFacturacion ObtenerXId(object o)
        {
            throw new NotImplementedException();
        }

        public DetalleFacturacion ObtenerXId(object[] a)
        {
            if (a != null)
            {
                return ObtenerTodos().FirstOrDefault(f => f.idFacturacion.Equals(a[0]) && f.idProducto.Equals(a[1]));
            }
            return new DetalleFacturacion();
        }
    }
}
