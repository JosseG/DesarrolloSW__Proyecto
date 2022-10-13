namespace LabPortugal_Intranet.Commons
{
    public interface ICrud<T>
    {

        T ObtenerXId(Object o);
        List<T> ObtenerTodos();
        void Actualizar(T o);
        void Agregar(T o);

        void Eliminar(Object o);

    }
}
