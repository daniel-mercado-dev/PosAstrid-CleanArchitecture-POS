using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<VentaCabecera> VentaCabeceras => Set<VentaCabecera>();
        // ... tus demás DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        
        // Al heredar de DbContext, el método SaveChangesAsync(CancellationToken) ya existe 
        // y cumple automáticamente con el contrato de IUnitOfWork.
    }
}