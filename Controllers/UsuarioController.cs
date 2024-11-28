
using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using GestionInventario.Datos;
using GestionInventario.Datos.DTOs;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UserCreateDTO userDto)
        {
            var usuario = new Usuario
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Nombre = userDto.Nombre,
                Apellido = userDto.Apellido,
                TipoDocumento = userDto.TipoDocumento,
                NumeroDocumento = userDto.NumeroDocumento,
                Direccion = userDto.Direccion,
                Telefono = userDto.Telefono,
                EstadoActivo = userDto.EstadoActivo
            };

            var result = await _usuarioServicio.CrearUsuarioAsync(usuario, userDto.Password, userDto.Rol);
            if (result.Succeeded)
            {
                return Ok(new { usuario.Id, usuario.Nombre, usuario.Direccion, usuario.Telefono });
            }
            return BadRequest(result.Errors);
            
            {
                // Devolver la representación del usuario creado
                return Ok(new {
                    Id = usuario.Id, // Cambiado de UsuarioId para devolver solo los campos requeridos
                    Nombre = usuario.Nombre,
                    Direccion = usuario.Direccion,
                    Telefono = usuario.Telefono
                });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("BuscarPorEmail")]
        public async Task<IActionResult> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _usuarioServicio.ObtenerUsuarioPorEmailAsync(email);
            return usuario == null ? NotFound("Usuario no encontrado") : Ok(usuario);
        }
    }
}

    public class UserCreateDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool EstadoActivo { get; set; } = true;
        public string Rol { get; set; } = "Auxiliar"; // Se debe especificar si es "Administrador" o "Auxiliar".
    }
