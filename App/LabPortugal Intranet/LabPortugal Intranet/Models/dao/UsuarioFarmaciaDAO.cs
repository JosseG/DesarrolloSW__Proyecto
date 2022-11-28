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
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_usuario_farmacia_agregar @idfarmacia,@alias,@contrasena ", connection);
                    command.Parameters.AddWithValue("@idfarmacia", o.idFarmacia);
                    command.Parameters.AddWithValue("@alias", o.alias);
                    command.Parameters.AddWithValue("@contrasena", o.contrasena);

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
                            id  = reader.GetString(0),
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

            }
        }

        public UsuarioFarmacia ObtenerXId(object o)
        {
            Debug.WriteLine(o + " usuario farmacia");
            if (o != null)
            {
                /*Debug.WriteLine(ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o)).idGoogleAuth);*/
                return ObtenerTodos().FirstOrDefault(f => f.id.Equals(o));
            }
            return new UsuarioFarmacia();
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

                    SqlCommand command = new SqlCommand("select dbo.autogenerausuariofarmacia()", connection);

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


        public UsuarioFarmacia validarUsuario(string a, string b)
        {
            UsuarioFarmacia usuarioFarmacia = null;
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

                    if (reader.Read())
                    {
                        Debug.WriteLine("Si llego");
                        usuarioFarmacia=new UsuarioFarmacia();
                        usuarioFarmacia.id = reader.GetString(0);
                        usuarioFarmacia.idFarmacia = reader.GetString(1);
                        usuarioFarmacia.alias = reader.GetString(2);
                        usuarioFarmacia.contrasena = reader.GetString(3);
                        usuarioFarmacia.estado = reader.GetBoolean(4);

                        Debug.WriteLine("En lectura f" + usuarioFarmacia.idFarmacia);
                        Debug.WriteLine("En lectura a" + usuarioFarmacia.alias);
                        Debug.WriteLine("En lectura c" + usuarioFarmacia.contrasena);
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return usuarioFarmacia;
            }
        }

    }
}
