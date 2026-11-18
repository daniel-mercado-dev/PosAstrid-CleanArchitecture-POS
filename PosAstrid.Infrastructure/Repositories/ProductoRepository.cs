using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Entities;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PosAstrid.Infrastructure.Repositories
{
    public sealed class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Productos
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Productos.ToListAsync(cancellationToken);
        }

        public void Agregar(Producto producto)
        {
            _dbContext.Productos.Add(producto);
        }

        public void Actualizar(Producto producto)
        {
            _dbContext.Productos.Update(producto);
        }
    }
}