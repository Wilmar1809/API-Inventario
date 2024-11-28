
using Microsoft.AspNetCore.Mvc;
using GestionInventario.Datos.Negocio.Servicios;
using Microsoft.AspNetCore.Identity; // Para UserManager y Identity
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims; // Para Claim y ClaimTypes
using Microsoft.Extensions.Configuration;
using GestionInventario.Datos; // Espacio de nombres para el modelo Usuario

namespace GestionInventario.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticarController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public AutenticarController(IUsuarioServicio usuarioServicio, UserManager<Usuario> userManager, IConfiguration configuration)
        {
            _usuarioServicio = usuarioServicio;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginRequest request)
        {
            bool esValido = await _usuarioServicio.ValidarCredencialesAsync(request.Email, request.Password);
            if (!esValido)
            {
                return Unauthorized("Credenciales incorrectas");
            }

            // Obtener el usuario a partir del email proporcionado
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Obtener roles del usuario
            var roles = await _userManager.GetRolesAsync(usuario);
            
            // Definir los claims para el token, incluyendo el rol del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? "Auxiliar") // Aquí se toma el primer rol, si existe, sino se asigna "Auxiliar"
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}


