using LabPortugal_Intranet.Commons;
using LabPortugal_Intranet.Commons.Imp;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LabPortugal_Intranet.Models.dao
{
    public class GoogleFarmaciaDAO : ICrud<GoogleFarmacia>
    {
        public void Actualizar(GoogleFarmacia o)
        {
            throw new NotImplementedException();
        }

        public void Agregar(GoogleFarmacia o)
        {
            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {

                    SqlCommand command = new SqlCommand("exec usp_google_farmacia_agregar @idGoogle,@idfarmacia ", connection);
                    command.Parameters.AddWithValue("@idGoogle", o.idGoogleAuth);
                    command.Parameters.AddWithValue("@idfarmacia", o.idFarmacia);


                    int count = command.ExecuteNonQuery();
                    Debug.WriteLine("Agregar Google " + count);
                    Debug.WriteLine(o.idGoogleAuth + " " + o.idFarmacia);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString() + " hola");

                }
            }
        }

        public void Eliminar(object o)
        {
            throw new NotImplementedException();
        }

        public List<GoogleFarmacia> ObtenerTodos()
        {
            var listaFarmacias = new List<GoogleFarmacia>();

            SqlConnection connection = new Conexion().getConnection();
            using (connection)
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand("exec usp_google_farmacias_listar", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaFarmacias.Add(new GoogleFarmacia()
                        {
                            idGoogleAuth = reader.GetString(0),
                            idFarmacia = reader.GetString(1),
                            estado = reader.GetBoolean(2)
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return listaFarmacias;

            }


        }

        public GoogleFarmacia ObtenerXId(object o)
        {
            Debug.WriteLine(o + " jekke");
            if (o!=null)
            {
                /*Debug.WriteLine(ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o)).idGoogleAuth);*/
                return ObtenerTodos().FirstOrDefault(f => f.idGoogleAuth.Equals(o));
            }
            return new GoogleFarmacia();
        }
    }
}
