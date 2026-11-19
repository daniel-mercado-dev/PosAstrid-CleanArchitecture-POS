using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;

namespace PosAstrid.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> ObtenerTodasAsync(CancellationToken ct = default)
        {
            return await _context.Set<Categoria>().Where(c => c.Activo).ToListAsync(ct);
        }

        public async Task<Categoria?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Set<Categoria>().FindAsync(new object[] { id }, ct);
        }

        public void Agregar(Categoria categoria)
        {
            _context.Set<Categoria>().Add(categoria);
        }
    }
}