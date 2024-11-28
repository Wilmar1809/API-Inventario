
namespace GestionInventario.Datos
{
    public static class GeneradorDatos
    {
        private static readonly Random Random = new Random();

        public static string NombreAleatorio()
        {
            var nombres = new[] { "Juan", "Pedro", "Ana"};
            return nombres[Random.Next(nombres.Length)];
        }

        public static string ApellidoAleatorio()
        {
            var apellidos = new[] { "Garcia", "Lopez", "Diaz"};
            return apellidos[Random.Next(apellidos.Length)];
        }

        public static string EmailAleatorio(string nombre, string apellido)
        {
            var dominio = new[] {"msn.com", "abc.com"};
            return $"{nombre.ToLower()}.{apellido.ToLower()}@{dominio[Random.Next(dominio.Length)]}";
        }

        public static string TelefonoAleatorio()
        {
            return $"312 123 456 {Random.Next(150, 200)}";
        }

        public static string DocumentoAleatorio()
        {
            return $"{Random.Next(12345, 99999)}";
        }

        public static string TipoAleatorio()
        {
            var tipos = new[] { "C.C", "C.E", "T.I"};
            return tipos[Random.Next(tipos.Length)];
        }

        public static string DireccionAleatoria()
        {
            return $"Calle {Random.Next(1, 50)}, # {Random.Next(1, 50)}";
        }

        public static string ContraseñaAleatorio()
        {
            return $"Contraseña{Random.Next(1000, 9999)}";
        }
    }
}