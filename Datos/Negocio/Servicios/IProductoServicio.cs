/*
using GestionInventario.Datos.DTOs;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IProductoServicio
    {
        void CrearProducto(Producto producto);
        Producto ObtenerProducto(int id);
        List<Producto> ObtenerTodos();
        void ModificarProducto(Producto producto);
        void EliminarProducto(int id);
        void ActualizarInventario(int productoId, int cantidad, string tipoMovimiento, decimal valorUnitario, string motivo);
        void ActualizarInventario(int id, int cantidad, string tipoMovimiento, string motivo);
        List<ReporteInventarioDTO> GenerarReporteInventario(int? productoId);
    }
}*/

using GestionInventario.Datos.DTOs;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IProductoServicio
    {
        void CrearProducto(Producto producto);
        Producto ObtenerProducto(int id);
        List<Producto> ObtenerTodos();
        void ModificarProducto(Producto producto);
        void EliminarProducto(int id);
        void ActualizarInventario(int productoId, int cantidad, string tipoMovimiento, decimal valorUnitario, string motivo);
        List<ReporteInventarioDTO> GenerarReporteInventario(int? productoId);
        void ModificarProducto(Producto producto, string? rolUsuario);
        void EliminarProducto(int id, string? rolUsuario);

        // Nuevo método para verificar el nivel de stock
        //bool VerificarNivelStock(int productoId, int nivelMinimo);
    }
}

