
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GestionInventario.Datos.DTOs;

namespace GestionInventario.Utilidades
{
    public static class ExportHelper
    {
        public static string GenerarCSV(List<ReporteInventarioDTO> reporte)
        {
            var csv = new StringBuilder();

            // Encabezados
            csv.AppendLine("producto,saldo,costopromedio,costototal");

            // Datos
            foreach (var item in reporte)
            {
                // Asegurarse de que cada valor esté alineado con los encabezados
                // Se utiliza InvariantCulture para asegurar el formato correcto de números decimales
                csv.AppendLine(string.Join(",",
                    item.Producto,
                    item.Saldo.ToString(CultureInfo.InvariantCulture),
                    item.CostoPromedio.ToString("F2", CultureInfo.InvariantCulture),
                    item.CostoTotal.ToString("F2", CultureInfo.InvariantCulture)

                // Asegúrate de que todos los valores estén alineados con sus respectivas columnas
                //csv.AppendLine($"{item.Producto},{item.Saldo},{item.CostoPromedio},{item.CostoTotal}");
                ));
            }

            return csv.ToString();
        }
    }
}
