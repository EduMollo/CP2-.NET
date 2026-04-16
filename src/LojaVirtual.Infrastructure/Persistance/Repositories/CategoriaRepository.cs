using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(LojaVirtualContext context) : base(context) { }

        public async Task<Categoria?> GetCategoriaComProdutosAsync(Guid categoriaId)
        {
            return await _dbSet
                .Include(c => c.CategoriaProdutos)
                .FirstOrDefaultAsync(c => c.Id == categoriaId);
        }

        public async Task<IReadOnlyCollection<Categoria>> GetCategoriasComProdutosAsync()
        {
            return await _dbSet
                .Include(c => c.CategoriaProdutos)
                .Where(c => c.Active)
                .ToListAsync();
        }
    }
}
