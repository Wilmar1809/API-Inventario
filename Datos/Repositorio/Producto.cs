
namespace GestionInventario.Datos
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; } =string.Empty; // Inicialización predeterminada
        public required string Categoria { get; set; } = string.Empty; // Inicialización predeterminada
        public int ProveedorId { get; set; }

        // Propiedad de navegación
        public Proveedor Proveedor { get; set; }
        public int StockActual { get; internal set; }
        //public decimal PrecioUnitario { get; internal set; }
        //public decimal CostoTotal { get; internal set; }
        //public decimal PrecioVenta { get; internal set; }
    }
}