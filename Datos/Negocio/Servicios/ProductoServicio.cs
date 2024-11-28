
using GestionInventario.Datos;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos.Repositorio;
using GestionInventario.Datos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IMovimientoServicio _movimientoServicio;

        public ProductoServicio(IProductoRepositorio productoRepositorio, IMovimientoServicio movimientoServicio)
        {
            _productoRepositorio = productoRepositorio;
            _movimientoServicio = movimientoServicio;
        }

        public void CrearProducto(Producto producto) => _productoRepositorio.CrearProducto(producto);

        public Producto ObtenerProducto(int id) => _productoRepositorio.ObtenerProducto(id);

        public List<Producto> ObtenerTodos() => _productoRepositorio.ObtenerTodos();

        public void ModificarProducto(Producto producto, string rolUsuario) //=> _productoRepositorio.ModificarProducto(producto);
        {
            //if (rolUsuario != "Administrador")
            //{
                //throw new UnauthorizedAccessException("No tienes permiso para modificar productos.");
            //}

            _productoRepositorio.ModificarProducto(producto);
        }

        public void EliminarProducto(int id, string rolUsuario) //=> _productoRepositorio.EliminarProducto(id);
        {
            //if (rolUsuario != "Administrador")
            //{
                //throw new UnauthorizedAccessException("No tienes permiso para eliminar productos.");
            //}
            _productoRepositorio.EliminarProducto(id);
        }
  
        public void ValidarPermisos(string rol, bool permitirEdicion)
         {
            if (string.IsNullOrEmpty(rol))
            {
                throw new ArgumentNullException(nameof(rol), "El rol no puede ser nulo o vacío.");
            }
            if (rol != "Administrador" && permitirEdicion == false)
            {
                throw new UnauthorizedAccessException("No tienes permiso para realizar esta acción.");
            }
         }

        public void ActualizarInventario(int productoId, int cantidad, string tipoMovimiento, decimal valorUnitario, string motivo)
        {
            var producto = _productoRepositorio.ObtenerProducto(productoId);
            if (producto == null) throw new Exception("Producto no encontrado");

            // Obtener la lista de movimientos anteriores
            var movimientosAnteriores = _movimientoServicio.ObtenerMovimientosPorProducto(productoId);

            // Crear el movimiento
            var movimiento = new Movimiento
            {
                ProductoId = productoId,
                Fecha = DateTime.Now,
                TipoMovimiento = tipoMovimiento,
                Cantidad = cantidad,
                ValorUnitario = valorUnitario,
                Motivo = motivo,
                ProveedorId = producto.ProveedorId,
                CostoTotal = cantidad* valorUnitario// Asignar el costo total correctamente
            };

            // Calcular el costo promedio ponderado y el precio de venta solo si es una entrada
            if (tipoMovimiento.ToLower() == "entrada")
            {
                movimiento.CalcularCostoTotal(movimientosAnteriores);
            }
            else
            {
                // Para salidas, mantener el PrecioVenta del último movimiento de entrada
                var ultimoMovimientoEntrada = movimientosAnteriores.LastOrDefault(m => m.TipoMovimiento.ToLower() == "entrada");
                if (ultimoMovimientoEntrada != null)
                {
                    movimiento.PrecioVenta = ultimoMovimientoEntrada.PrecioVenta;
                }
            }

            // Registrar el movimiento
            _movimientoServicio.RegistrarMovimiento(movimiento);
        }

        public void ActualizarInventario(int id, int cantidad, string tipoMovimiento, string motivo)
        {
            throw new NotImplementedException();
        }

        public List<ReporteInventarioDTO> GenerarReporteInventario(int? productoId)
        {
            var productos = productoId.HasValue 
                ? new List<Producto> { _productoRepositorio.ObtenerProducto(productoId.Value) } 
                : _productoRepositorio.ObtenerTodos();

            var reporte = new List<ReporteInventarioDTO>();

            foreach (var producto in productos)
            {
                var movimientos = _movimientoServicio.ObtenerMovimientosPorProducto(producto.Id);

                   // Considerar solamente las entradas para el cálculo del saldo
                var saldo = movimientos.Sum(m => m.TipoMovimiento.ToLower() == "entrada" ? m.Cantidad : -m.Cantidad);
                var entradas = movimientos.Where(m => m.TipoMovimiento.ToLower() == "entrada").ToList();
                decimal costoPromedio = entradas.Any() ? entradas.Average(m => m.ValorUnitario) : 0;
                decimal costoTotal = saldo * costoPromedio;
                //var saldo = movimientos.Sum(m => m.TipoMovimiento.ToLower() == "entrada" ? m.Cantidad : -m.Cantidad);
                //var costoPromedio = movimientos.Where(m => m.TipoMovimiento.ToLower() == "entrada").Average(m => m.ValorUnitario);
                //var costoTotal = saldo * costoPromedio;

                reporte.Add(new ReporteInventarioDTO
                {
                    Producto = producto.Nombre,
                    Saldo = saldo,
                    CostoPromedio = costoPromedio,
                    CostoTotal = costoTotal
                });
            }

            return reporte;
        }

        public bool VerificarNivelStock(int productoId, int nivelMinimo)
        {
            throw new NotImplementedException();
        }

        public void ModificarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public void EliminarProducto(int id)
        {
            throw new NotImplementedException();
        }
    }
}