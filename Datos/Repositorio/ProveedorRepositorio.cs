
using System.Collections.Generic;
using System.Linq;
using GestionInventario.Datos;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos.Repositorio
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly GestionInventarioDbContext _context;

        public ProveedorRepositorio(GestionInventarioDbContext context)
        {
            _context = context;
        }

        public void CrearProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }

        public Proveedor ObtenerProveedor(int id)
        {
            return _context.Proveedores.Find(id);
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _context.Proveedores.ToList();
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            var proveedorExistente = _context.Proveedores.Find(proveedor.Id);
            if (proveedorExistente != null)
            {
                proveedorExistente.Nombre = proveedor.Nombre;
                proveedorExistente.Direccion = proveedor.Direccion;
                proveedorExistente.Telefono = proveedor.Telefono;

                _context.Proveedores.Update(proveedorExistente);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
            else
            {
                throw new KeyNotFoundException("El proveedor no se encontró.");
            }
        }

        public void EliminarProveedor(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
            else
            {
                throw new KeyNotFoundException("El proveedor no se encontró.");
            }
        }
    }
}

