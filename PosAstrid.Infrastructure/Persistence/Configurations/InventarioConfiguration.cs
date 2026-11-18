using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Persistence.Configurations
{
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
            builder.ToTable("InventarioDiario");

            builder.Property(i => i.Vendido)
                .HasComputedColumnSql("\"StockInicial\" - \"StockFinal\"", stored: true);

            builder.Property(i => i.MontoTotal)
                .HasComputedColumnSql("(\"StockInicial\" - \"StockFinal\") * \"PrecioUnitario\"", stored: true);
        }
    }
}