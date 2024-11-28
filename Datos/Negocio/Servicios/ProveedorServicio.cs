
using GestionInventario.Datos.Repositorio;
using GestionInventario.Datos;
using System.Collections.Generic;
using System;

namespace GestionInventario.Datos.Negocio.Servicios
{
    public class ProveedorServicio : IProveedorServicio
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;

        public ProveedorServicio(IProveedorRepositorio proveedorRepositorio)
        {
            _proveedorRepositorio = proveedorRepositorio;
        }

        public void CrearProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo.");
            }

            _proveedorRepositorio.CrearProveedor(proveedor);
        }

        public Proveedor ObtenerProveedor(int id)
        {
            return _proveedorRepositorio.ObtenerProveedor(id);
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _proveedorRepositorio.ObtenerTodos();
        }

        public void ModificarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo.");
            }

            var proveedorExistente = _proveedorRepositorio.ObtenerProveedor(proveedor.Id);
            if (proveedorExistente == null)
            {
                throw new KeyNotFoundException("El proveedor no existe.");
            }

            _proveedorRepositorio.ModificarProveedor(proveedor);
        }

        public void EliminarProveedor(int id)
        {
            var proveedorExistente = _proveedorRepositorio.ObtenerProveedor(id);
            if (proveedorExistente == null)
            {
                throw new KeyNotFoundException("El proveedor no existe.");
            }

            _proveedorRepositorio.EliminarProveedor(id);
        }

        public void ModificarProveedor(Proveedor proveedor, string? rolUsuario)
        {
            throw new NotImplementedException();
        }

        public void EliminarProveedor(int id, string? rolUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
