using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;

namespace PosAstrid.Infrastructure.Repositories;

public sealed class InventarioRepository : IInventarioRepository
{
    private readonly AppDbContext _db;
    public InventarioRepository(AppDbContext db) => _db = db;

    public Task<Inventario?> ObtenerPorProductoYFechaAsync(int productoId, DateTime fecha, CancellationToken ct = default)
        => _db.Inventarios.FirstOrDefaultAsync(i => i.ProductoId == productoId && i.Fecha == fecha, ct);

    public void Agregar(Inventario inventario) => _db.Inventarios.Add(inventario);
}