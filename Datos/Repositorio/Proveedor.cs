
namespace GestionInventario.Datos
{
    public class Proveedor 
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = string.Empty; // Inicialización predeterminada
        public required string Direccion { get; set; } = string.Empty; // Inicialización predeterminada
        public required string Telefono { get; set; } = string.Empty; // Inicialización predeterminada

        // Relación con productos
        public List<Producto> Productos { get; set; } = new();
    }
}