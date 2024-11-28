
namespace GestionInventario.Datos
{
    public class Movimiento
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public DateTime Fecha { get; set; }
        public required string TipoMovimiento { get; set; } = string.Empty;// "entrada" o "salida"
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; } // Campo agregado para reflejar el valor unitario en el movimiento
        public required string Motivo { get; set; } = string.Empty;
        public int ProveedorId { get; set; }
        public decimal CostoTotal { get; set; } // Campo para almacenar el costo total correctamente calculado
        public decimal PrecioVenta { get; set; } // Campo para almacenar el precio de venta calculado

        // Propiedades de navegación
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }

        // Método para calcular el costo total y el precio de venta basado en la fórmula proporcionada
        public void CalcularCostoTotal(List<Movimiento> movimientosAnteriores)
        {
             // Solo considerar movimientos de entrada para calcular el costo promedio ponderado
            var movimientosEntrada = movimientosAnteriores.Where(m => m.TipoMovimiento.ToLower() == "entrada").ToList();

            decimal sumaValorUnitarioPorCantidad = movimientosEntrada.Sum(m => m.Cantidad * m.ValorUnitario) + (TipoMovimiento.ToLower() == "entrada" ? Cantidad * ValorUnitario : 0);

              // Sumar solo las cantidades de las entradas
            decimal sumaTotalCantidad = movimientosEntrada.Sum(m => m.Cantidad) + (TipoMovimiento.ToLower() == "entrada" ? Cantidad : 0);
            //decimal sumaValorUnitarioPorCantidad = movimientosAnteriores.Sum(m => m.Cantidad * m.ValorUnitario) + (Cantidad * ValorUnitario);

            // Aseguramos que se sume correctamente la cantidad actual * valor unitario al valor acumulado.
            //decimal sumaTotalCantidad = movimientosAnteriores.Sum(m => m.Cantidad) + Cantidad;

            if (sumaTotalCantidad > 0) // Prevenir división por cero
            {
                //CostoTotal = sumaValorUnitarioPorCantidad; // Corregido para asignar CostoTotal
                PrecioVenta = sumaValorUnitarioPorCantidad / sumaTotalCantidad;
            }
            else
            {
                PrecioVenta = ValorUnitario; // Si no hay inventario anterior, el precio de venta es igual al valor unitario de la nueva entrada.
            }

            // Calcular el precio de venta basado solo si es una entrada
            if (TipoMovimiento.ToLower() == "entrada")
            {
                //PrecioVenta = ValorUnitario; // Establecer el precio de venta igual al costo total para entradas
            }
                 // Calcular CostoTotal para reflejar el costo acumulado
                CostoTotal = Cantidad * ValorUnitario;
        }
    }
}

