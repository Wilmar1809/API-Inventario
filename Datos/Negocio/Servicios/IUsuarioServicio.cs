
using System.Threading.Tasks;
using GestionInventario.Datos;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public interface IUsuarioServicio
    {
        Task<bool> ValidarCredencialesAsync(string email, string password);
        Task<Usuario> ObtenerUsuarioPorEmailAsync(string email);
        Task<IdentityResult> CrearUsuarioAsync(Usuario usuario, string password, string rol);
        //Task CrearUsuarioAsync(Usuario usuario, string password, string rol);
    }
}