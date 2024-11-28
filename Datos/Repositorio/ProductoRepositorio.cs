using System.Collections.Generic;
using System.Linq;
using GestionInventario.Datos;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Repositorio
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly GestionInventarioDbContext _context;

        public ProductoRepositorio(GestionInventarioDbContext context)
        {
            _context = context;
        }

        public void CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        public Producto ObtenerProducto(int id)
        {
            return _context.Productos.Find(id);
        }

        public List<Producto> ObtenerTodos()
        {
            return _context.Productos.ToList();
        }

        public void ModificarProducto(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        public void EliminarProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }
    }
}
