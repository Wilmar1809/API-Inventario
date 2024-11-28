
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioServicio(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        // Implementación de CrearUsuarioAsync que devuelve IdentityResult
        public async Task<IdentityResult> CrearUsuarioAsync(Usuario usuario, string password, string rol)
        {
            var resultado = await _userManager.CreateAsync(usuario, password);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, rol);
            }
            return resultado;
        }

        public Task<IdentityResult> CrearUsuarioAsync(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ValidarCredencialesAsync(string email, string password)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            if (usuario == null) return false;

            return await _userManager.CheckPasswordAsync(usuario, password);
        }

        public Task<bool> ValidarCredencialesAsync(string email, string password, string rol)
        {
            throw new NotImplementedException();
        }

        //Task IUsuarioServicio.CrearUsuarioAsync(Usuario usuario, string password, string rol)
        //{
            //throw new NotImplementedException();
        //}
    }
}
