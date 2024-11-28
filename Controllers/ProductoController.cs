
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;
using GestionInventario.Datos.DTOs;
using GestionInventario.Utilidades;
using System.Security.Claims;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IMovimientoServicio _movimientoServicio;

        public ProductoController(IProductoServicio productoServicio, IMovimientoServicio movimientoServicio)
        {
            _productoServicio = productoServicio;
            _movimientoServicio = movimientoServicio;
        }

        [Authorize(Roles = "Administrador,Auxiliar")]
        [HttpPost("crear")]
        public IActionResult CrearProducto([FromBody] ProductoCreateDTO productoDto)
        {
            // Validar los datos del DTO
            if (string.IsNullOrWhiteSpace(productoDto.Nombre) ||
                string.IsNullOrWhiteSpace(productoDto.Categoria) ||
                productoDto.ProveedorId <= 0)
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            // Mapear ProductoCreateDTO a Producto
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Categoria = productoDto.Categoria,
                ProveedorId = productoDto.ProveedorId
            };

            try
            {
                // Llamar al servicio para crear el producto
                _productoServicio.CrearProducto(producto);

                // Devolver la representación del producto creado
                return Ok(new
                {
                    mensaje = "Producto creado exitosamente",
                    producto = new
                    {
                        producto.Id,
                        producto.Nombre,
                        producto.Categoria,
                        producto.ProveedorId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProducto(int id)
        {
            var producto = _productoServicio.ObtenerProducto(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            var movimientos = _movimientoServicio.ObtenerMovimientosPorProducto(id);
            var ultimoMovimiento = movimientos.OrderByDescending(m => m.Fecha).FirstOrDefault();
            decimal precioVenta = ultimoMovimiento?.PrecioVenta ?? 0;

            return Ok(new
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                ProveedorId = producto.ProveedorId,
                PrecioVenta = precioVenta // Mostrar el precio de venta
            });
        }

        [HttpGet("todos")]
        public IActionResult ObtenerTodos() => Ok(_productoServicio.ObtenerTodos());

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] ProductoCreateDTO productoDto)
        {
           var rolUsuario = User.FindFirst(ClaimTypes.Role)?.Value;

           if (rolUsuario != "administrador")
           {
            return Unauthorized("No tiene permiso para modificar productos.");
           }
           var producto = new Producto
           {
               Id = id,
               Nombre = productoDto.Nombre,
               Categoria = productoDto.Categoria,
               ProveedorId = productoDto.ProveedorId
            
            };
            _productoServicio.ModificarProducto(producto);
            return Ok("Producto modificado exitosamente");

        }
        
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var rolUsuario = User.FindFirst(ClaimTypes.Role)?.Value;
            if (rolUsuario != "Administrador")
            {
                return Forbid("No tiene permiso para eliminar productos");
            }

            _productoServicio.EliminarProducto(id, rolUsuario);
            return Ok("Producto eliminado exitosamente");
        }

        [HttpPost("{id}/actualizar-inventario")]
        public IActionResult ActualizarInventario(int id, int cantidad, string tipoMovimiento, decimal valorUnitario, string motivo)
        {
            try
            {
                decimal costoTotal = cantidad * valorUnitario;
                _productoServicio.ActualizarInventario(id, cantidad, tipoMovimiento, valorUnitario, motivo);
                return Ok("Inventario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("reporte-inventario")]
        public IActionResult GenerarReporteInventario(int? productoId = null)
        {
            try
            {
                var reporte = _productoServicio.GenerarReporteInventario(productoId);
                var csv = ExportHelper.GenerarCSV(reporte);
                var nombreArchivo = $"ReporteInventario_{DateTime.Now:yyyyMMddHHmmss}.csv";

                return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    public class ProductoCreateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int ProveedorId { get; set; }
    }
}



















