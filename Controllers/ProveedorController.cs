
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;
using System.Security.Claims;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorServicio _proveedorServicio;

        public ProveedorController(IProveedorServicio proveedorServicio)
        {
            _proveedorServicio = proveedorServicio;
        }

        [Authorize(Roles = "Administrador,Auxiliar")]
        [HttpPost("crear")]
        public IActionResult CrearProveedor([FromBody] ProveedorCreateDTO proveedorDto)
        {
            if (string.IsNullOrWhiteSpace(proveedorDto.Nombre) || 
                string.IsNullOrWhiteSpace(proveedorDto.Direccion) || 
                string.IsNullOrWhiteSpace(proveedorDto.Telefono))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            var proveedor = new Proveedor
            {
                Nombre = proveedorDto.Nombre,
                Direccion = proveedorDto.Direccion,
                Telefono = proveedorDto.Telefono
            };

            try
            {
                _proveedorServicio.CrearProveedor(proveedor);
                return Ok(new { mensaje = "Proveedor creado exitosamente", proveedor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerProveedor(int id)
        {
            var proveedor = _proveedorServicio.ObtenerProveedor(id);
            return proveedor == null 
                ? NotFound("Proveedor no encontrado") 
                : Ok(new 
                {
                    Id = proveedor.Id,
                    Nombre = proveedor.Nombre,
                    Direccion = proveedor.Direccion,
                    Telefono = proveedor.Telefono
                });
        }

        [HttpGet("todos")]
        public IActionResult ObtenerTodos() => Ok(_proveedorServicio.ObtenerTodos().Select(proveedor => new
        {
            Id = proveedor.Id,
            Nombre = proveedor.Nombre,
            Direccion = proveedor.Direccion,
            Telefono = proveedor.Telefono
        }));

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult ModificarProveedor(int id, [FromBody] ProveedorCreateDTO proveedorDto)
        {
            if (string.IsNullOrWhiteSpace(proveedorDto.Nombre) || 
                string.IsNullOrWhiteSpace(proveedorDto.Direccion) || 
                string.IsNullOrWhiteSpace(proveedorDto.Telefono))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            var proveedor = new Proveedor
            {
                Id = id,
                Nombre = proveedorDto.Nombre,
                Direccion = proveedorDto.Direccion,
                Telefono = proveedorDto.Telefono
            };

            try
            {
                _proveedorServicio.ModificarProveedor(proveedor);
                return Ok("Proveedor modificado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult EliminarProveedor(int id)
        {
            try
            {
                _proveedorServicio.EliminarProveedor(id);
                return Ok("Proveedor eliminado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    public class ProveedorCreateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}
