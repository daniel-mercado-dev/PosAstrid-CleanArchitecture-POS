using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;

namespace PosAstrid.Infrastructure.Repositories
{
    public sealed class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VentaCabecera?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<VentaCabecera>()
                .Include(v => v.Detalles)
                .Include(v => v.Cliente)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<VentaCabecera>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<VentaCabecera>()
                .AsNoTracking()
                .Include(v => v.Detalles)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(VentaCabecera venta, CancellationToken cancellationToken = default)
        {
            await _context.Set<VentaCabecera>().AddAsync(venta, cancellationToken);
        }
    }
}