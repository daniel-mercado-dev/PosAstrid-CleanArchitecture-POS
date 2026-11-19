using PosAstrid.Domain.Entities;

namespace PosAstrid.Domain.Repositories
{
    public interface IVentaRepository
    {
        Task<VentaCabecera?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<VentaCabecera>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(VentaCabecera venta, CancellationToken cancellationToken = default);
    }
}