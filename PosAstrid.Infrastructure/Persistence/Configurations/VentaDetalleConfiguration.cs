using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Persistence.Configurations
{
    public sealed class VentaDetalleConfiguration : IEntityTypeConfiguration<VentaDetalle>
    {
        public void Configure(EntityTypeBuilder<VentaDetalle> builder)
        {
            builder.ToTable("VentaDetalles");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.VentaCabeceraId)
                .IsRequired();

            builder.Property(d => d.ProductoId);

            builder.Property(d => d.ProductoNombre)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(d => d.Cantidad)
                .IsRequired();

            builder.Property(d => d.PrecioUnitario)
                .HasColumnType("numeric(12,2)")
                .IsRequired();

           

            builder.HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}