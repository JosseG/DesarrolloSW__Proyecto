using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using LabPortugal_Intranet.Models;
using LabPortugal_Intranet.Commons.Imp;
using LabPortugal_Intranet.Commons;

namespace LabPortugal_Intranet.Models.dao
{
    public class TipoProductoDAO : ICrud<TipoProducto>
    {
        public void Actualizar(TipoProducto o)
        {
            throw new NotImplementedException();
        }

        public void Agregar(TipoProducto o)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(object o)
        {
            throw new NotImplementedException();
        }

        public List<TipoProducto> ObtenerTodos()
        {
            var list = new List<TipoProducto>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_tipoproducto_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new TipoProducto()
                        {
                            id = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            estado = reader.GetBoolean(2)
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

        public TipoProducto ObtenerXId(object o)
        {
            throw new NotImplementedException();
        }
    }
}
