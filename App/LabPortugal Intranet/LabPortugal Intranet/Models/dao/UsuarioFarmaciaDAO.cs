using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class UsuarioFarmaciaDAO : ICrud<UsuarioFarmacia>
    {
        public void Actualizar(UsuarioFarmacia o)
        {
            throw new NotImplementedException();
        }

        public void Agregar(UsuarioFarmacia o)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(object o)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioFarmacia> ObtenerTodos()
        {
            var list = new List<UsuarioFarmacia>();
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
                        list.Add(new UsuarioFarmacia()
                        {
                            id  = reader.GetInt32(0),
                            idFarmacia = reader.GetString(1),
                            alias = reader.GetString(2),
                            contrasena = reader.GetString(3),
                            estado = reader.GetBoolean(4)
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

        public UsuarioFarmacia ObtenerXId(object o)
        {
            throw new NotImplementedException();
        }

        public bool validarUsuario(string a, string b)
        {
            bool result = false;
            //var usuarioFarmacia = new UsuarioFarmacia() ;
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_usuario_farmacia_validar_u @alias, @contrasena", connection);

                    command.Parameters.AddWithValue("@alias", a);
                    command.Parameters.AddWithValue("@contrasena", b);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.NextResult())
                    {
                        Debug.WriteLine("Si llego");
                        result = true;
                        //usuarioFarmacia.id = reader.GetInt32(0);
                        //usuarioFarmacia.idFarmacia = reader.GetString(1);
                        //usuarioFarmacia.alias = reader.GetString(2);
                        //usuarioFarmacia.contrasena = reader.GetString(3);
                        //usuarioFarmacia.estado = reader.GetBoolean(4);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return result;
            }
        }

    }
}
