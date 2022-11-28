using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class CargoUsuarioFarmaciaDAO
    {


        public void Agregar(CargoUsuarioFarmacia o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO tb_cargo_usuario_farmacia values (2,@idfarmacia); ", connection);
                    command.Parameters.AddWithValue("@idfarmacia", o.idFarmacia);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());

                }
            }
        }

        public List<CargoUsuarioFarmacia> ObtenerTodos()
        {
            var list = new List<CargoUsuarioFarmacia>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("select * from tb_cargo_usuario_farmacia", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new CargoUsuarioFarmacia()
                        {
                            idCargo = reader.GetInt32(0),
                            idFarmacia = reader.GetString(1),
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



        public List<CargoUsuarioFarmacia> ObtenerTodosCargoXUsuarioFarmacia(string id)
        {
            var list = new List<CargoUsuarioFarmacia>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("select * from tb_cargo_usuario_farmacia where id_usuario_farmacia = @id", connection);

                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new CargoUsuarioFarmacia()
                        {
                            idCargo = reader.GetInt32(0),
                            idFarmacia = reader.GetString(1),
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


        public CargoUsuarioFarmacia ObtenerXId(int o, string u)
        {
            Debug.WriteLine(o + " CargoUsuarioFarmaciaDAO");
            if (o > 0)
            {
                /*Debug.WriteLine(ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o)).idGoogleAuth);*/
                return ObtenerTodos().FirstOrDefault(c => c.idCargo == o && c.idFarmacia.Equals(u));
            }
            return new CargoUsuarioFarmacia();
        }
    }
}
