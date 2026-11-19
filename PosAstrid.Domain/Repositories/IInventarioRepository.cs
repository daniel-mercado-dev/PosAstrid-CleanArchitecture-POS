using PosAstrid.Domain.Entities;

namespace PosAstrid.Domain.Repositories;

public interface IInventarioRepository
{
    Task<Inventario?> ObtenerPorProductoYFechaAsync(int productoId, DateTime fecha, CancellationToken ct = default);
    void Agregar(Inventario inventario);
}