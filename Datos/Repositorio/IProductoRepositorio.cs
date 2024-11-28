namespace GestionInventario.Datos.Repositorio
{
    public interface IProductoRepositorio
    {
        void CrearProducto(Producto producto);
        Producto ObtenerProducto(int id);
        List<Producto> ObtenerTodos();
        void ModificarProducto(Producto producto);
        void EliminarProducto(int id);
    }
}
