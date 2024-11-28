
using GestionInventario.Datos.DTOs;

namespace GestionInventario.Datos.DTOs
{
    public class ReporteInventarioDTO
    {
        public string Producto { get; set; } = string.Empty; 
        public int Saldo { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoTotal { get; set; }
    }
}