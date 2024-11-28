using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using System;
using System.Collections.Generic;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class MovimientoServicio : IMovimientoServicio
    {
        private readonly IMovimientoRepositorio _movimientoRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;

        public MovimientoServicio(IMovimientoRepositorio movimientoRepositorio, IProductoRepositorio productoRepositorio)
        {
            _movimientoRepositorio = movimientoRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        public void RegistrarMovimiento(int productoId, string tipoMovimiento, int cantidad, string motivo)
        {
            var producto = _productoRepositorio.ObtenerProducto(productoId);
            if (producto == null) throw new Exception("Producto no encontrado");

            decimal valorUnitario = 0;
            decimal totalValorUnitario = cantidad * valorUnitario;

            var movimiento = new Movimiento
            {
                ProductoId = productoId,
                TipoMovimiento = tipoMovimiento,
                Cantidad = cantidad,
                ValorUnitario = valorUnitario,
                Motivo = motivo,
                Fecha = DateTime.Now,
                ProveedorId = producto.ProveedorId,
                CostoTotal = cantidad * valorUnitario // Corregido para asignar el valor total correctamente
            };

            // Registrar el movimiento
            _movimientoRepositorio.RegistrarMovimiento(movimiento);
        }

        public Movimiento ObtenerMovimiento(int id) => _movimientoRepositorio.ObtenerMovimiento(id);

        public List<Movimiento> ObtenerMovimientosPorProducto(int productoId) => _movimientoRepositorio.ObtenerMovimientosPorProducto(productoId);

        public List<Movimiento> ObtenerTodos() => _movimientoRepositorio.ObtenerTodos();

        public void RegistrarMovimiento(Movimiento movimiento)
        {
            _movimientoRepositorio.RegistrarMovimiento(movimiento);
        }
    }
}
