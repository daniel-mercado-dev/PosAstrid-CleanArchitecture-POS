using PosAstrid.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PosAstrid.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
        void Agregar(Producto producto);
        void Actualizar(Producto producto);
    }
}