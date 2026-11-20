using MediatR;
using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Inventario> Inventarios => Set<Inventario>();
        public DbSet<VentaCabecera> VentaCabeceras => Set<VentaCabecera>();
        public DbSet<VentaDetalle> VentaDetalles => Set<VentaDetalle>();

        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Categoria> Categoria => Set<Categoria>();
        public DbSet<CajaTurno> CajaTurno => Set<CajaTurno>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var resultado = await base.SaveChangesAsync(cancellationToken);

            var entidades = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Count > 0)
                .ToList();

            var eventos = entidades.SelectMany(e => e.GetDomainEvents()).ToList();
            entidades.ForEach(e => e.ClearDomainEvents());

            foreach (var evento in eventos)
                await _mediator.Publish(evento, cancellationToken);

            return resultado;
        }
    }
}