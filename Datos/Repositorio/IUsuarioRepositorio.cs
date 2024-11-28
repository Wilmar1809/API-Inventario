using System.Threading.Tasks;
using GestionInventario.Datos;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.Datos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObtenerUsuarioPorEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(Usuario user, string password);
    }
}
