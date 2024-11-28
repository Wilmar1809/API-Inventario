namespace GestionInventario.Datos.Repositorio
{
    public interface IProveedorRepositorio
    {
        void CrearProveedor(Proveedor proveedor);
        Proveedor ObtenerProveedor(int id);
        List<Proveedor> ObtenerTodos();
        void ModificarProveedor(Proveedor proveedor);
        void EliminarProveedor(int id);
    }
}
