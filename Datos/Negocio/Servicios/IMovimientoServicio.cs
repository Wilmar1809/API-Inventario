
using System.Collections.Generic;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IMovimientoServicio
    {
        void RegistrarMovimiento(int productoId, string tipoMovimiento, int cantidad, string motivo);
        Movimiento ObtenerMovimiento(int id);
        List<Movimiento> ObtenerMovimientosPorProducto(int productoId);
        List<Movimiento> ObtenerTodos();
        void RegistrarMovimiento(Movimiento movimiento);
    }
}
