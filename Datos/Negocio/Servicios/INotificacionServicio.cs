namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface INotificacionServicio
    {
        //void NotificarBajoStock(Producto producto);
        void EnviarNotificacion(string mensaje);
    }
}
