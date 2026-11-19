using PosAstrid.Domain.Entities;

namespace PosAstrid.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObtenerTodasAsync(CancellationToken ct = default);
        Task<Categoria?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
        void Agregar(Categoria categoria);
    }
}