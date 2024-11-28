using System.Collections.Generic;

namespace GestionInventario.Datos.Repositorio
{
    public interface IMovimientoRepositorio
    {
        void RegistrarMovimiento(Movimiento movimiento);
        Movimiento ObtenerMovimiento(int id);
        List<Movimiento> ObtenerMovimientosPorProducto(int productoId);
        List<Movimiento> ObtenerTodos();
    }
}
