using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Persistence.Configurations
{
    public sealed class VentaCabeceraConfiguration : IEntityTypeConfiguration<VentaCabecera>
    {
        public void Configure(EntityTypeBuilder<VentaCabecera> builder)
        {
            builder.ToTable("VentaCabeceras");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Fecha)
                .IsRequired();

            builder.Property(v => v.MontoTotal)
                .HasColumnType("numeric(12,2)")
                .IsRequired();

            builder.Property(v => v.MetodoPago)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(v => v.ClienteId);

            builder.Property(v => v.UsuarioId)
                .IsRequired();

            builder.Navigation(v => v.Detalles)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(v => v.Detalles)
                .WithOne(d => d.Cabecera)
                .HasForeignKey(d => d.VentaCabeceraId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Usuario)
                .WithMany(u => u.Ventas)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}