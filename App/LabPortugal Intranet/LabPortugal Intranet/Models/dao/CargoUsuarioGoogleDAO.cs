using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class CargoUsuarioGoogleDAO
    {

        public void Agregar(CargoUsuarioGoogle o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO tb_cargo_usuario_google values (2,@idgoogle); ", connection);
                    command.Parameters.AddWithValue("@idgoogle", o.idGoogle);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                }
            }
        }

        public List<CargoUsuarioGoogle> ObtenerTodos()
        {
            var list = new List<CargoUsuarioGoogle>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("select * from tb_cargo_usuario_google", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new CargoUsuarioGoogle()
                        {
                            idCargo = reader.GetInt32(0),
                            idGoogle = reader.GetString(1),
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


        public List<CargoUsuarioGoogle> ObtenerTodosCargoXUsuarioGoogle(string id)
        {
            var list = new List<CargoUsuarioGoogle>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("select * from tb_cargo_usuario_google where id_google_auth = @id", connection);

                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new CargoUsuarioGoogle()
                        {
                            idCargo = reader.GetInt32(0),
                            idGoogle = reader.GetString(1),
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

        public CargoUsuarioGoogle ObtenerXId(int o,string u)
        {
            Debug.WriteLine(o + " CargoUsuarioGoogle");
            if (o > 0)
            {
                /*Debug.WriteLine(ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o)).idGoogleAuth);*/
                return ObtenerTodos().FirstOrDefault(c => c.idCargo == o && c.idGoogle.Equals(u));
            }
            return new CargoUsuarioGoogle();
        }
    }
}
