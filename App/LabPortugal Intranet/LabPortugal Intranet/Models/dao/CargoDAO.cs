using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class CargoDAO
    {

        public List<Cargo> ObtenerTodos()
        {
            var list = new List<Cargo>();
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("select * from tb_cargo", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Cargo()
                        {
                            id = reader.GetInt32(0),
                            rol = reader.GetString(1),
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

            }
        }


        public Cargo ObtenerXId(int o)
        {
            Debug.WriteLine(o + " jekke");
            if (o >0)
            {
                /*Debug.WriteLine(ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o)).idGoogleAuth);*/
                return ObtenerTodos().FirstOrDefault(c => c.id==o);
            }
            return new Cargo();
        }

    }
}
