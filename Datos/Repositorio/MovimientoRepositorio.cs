
using System.Collections.Generic;
using System.Linq;
using GestionInventario.Datos;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Repositorio
{
    public class MovimientoRepositorio : IMovimientoRepositorio
    {
        private readonly GestionInventarioDbContext _context;

        public MovimientoRepositorio(GestionInventarioDbContext context)
        {
            _context = context;
        }

        public void RegistrarMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            _context.SaveChanges(); // Guarda el movimiento en la base de datos
        }

        public Movimiento ObtenerMovimiento(int id)
        {
            return _context.Movimientos.Find(id);
        }

        public List<Movimiento> ObtenerMovimientosPorProducto(int productoId)
        {
            return _context.Movimientos
                .Where(m => m.ProductoId == productoId)
                .OrderBy(m => m.Fecha)
                .ToList();
        }

        public List<Movimiento> ObtenerTodos()
        {
            return _context.Movimientos.ToList();
        }
    }
}
