
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GestionInventario.Datos;

namespace GestionInventario.Datos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuarioRepositorio(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Usuario> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(Usuario user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}

