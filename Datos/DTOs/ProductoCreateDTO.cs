
namespace GestionInventario.Datos.DTOs
{
    public class ProductoCreateDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } = string.Empty; // Inicialización predeterminada
        public required string Categoria { get; set; } = string.Empty; // Inicialización predeterminada
        public int ProveedorId { get; set; }
        //public decimal PrecioUnitario { get; set; } // Asegúrate de que esté presente si se necesita
        // Nuevas propiedades agregadas para manejar el inventario
        public int StockActual { get; set; }
        public int NivelMinimoStock { get; set; }  // Nueva propiedad
        public int NivelMaximoStock { get; set; }  // Nueva propiedad
    }
}