namespace LabPortugal_Intranet.Commons
{
    public interface ICrudExtension<T> : ICrud<T>
    {

        void Eliminar(Object []a);

        T ObtenerXId(Object []a);

    }
}
