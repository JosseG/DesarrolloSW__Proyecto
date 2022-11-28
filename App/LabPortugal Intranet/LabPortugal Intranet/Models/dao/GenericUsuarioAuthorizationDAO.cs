namespace LabPortugal_Intranet.Models.dao
{
    public class GenericUsuarioAuthorizationDAO
    {

        UsuarioFarmaciaDAO usuarioFarmaciaDAO=new UsuarioFarmaciaDAO();
        GoogleFarmaciaDAO googleFarmaciaDAO = new GoogleFarmaciaDAO();
        CargoUsuarioFarmaciaDAO cargoUsuarioFarmaciaDAO = new CargoUsuarioFarmaciaDAO();
        CargoUsuarioGoogleDAO cargoUsuarioGoogleDAO = new CargoUsuarioGoogleDAO();

        CargoDAO cargoDAO = new CargoDAO();

        public GenericUsuarioFarmaciaAuthorizationModel obtenerGenericUsuarioConRoles(string id)
        {

            GenericUsuarioFarmaciaAuthorizationModel usuarioGenericoModel = new GenericUsuarioFarmaciaAuthorizationModel();

            GoogleFarmacia googleFarmacia = googleFarmaciaDAO.ObtenerXId(id);
            UsuarioFarmacia usuarioFarmacia = usuarioFarmaciaDAO.ObtenerXId(id);

            if (googleFarmacia.idGoogleAuth =="")
            {

            }
            else
            {
                
            }

            



            return usuarioGenericoModel;
        }



    }
}
