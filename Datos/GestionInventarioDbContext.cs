using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos
{
    public class GestionInventarioDbContext : IdentityDbContext<Usuario>
    {
        public GestionInventarioDbContext(DbContextOptions<GestionInventarioDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.CostoTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.PrecioVenta) // Configuración para el nuevo campo PrecioVenta
                .HasPrecision(18, 2);
        }
    }
}









