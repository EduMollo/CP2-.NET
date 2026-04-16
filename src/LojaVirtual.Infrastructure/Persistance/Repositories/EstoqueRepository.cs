using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(LojaVirtualContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Estoque>> GetEstoquesPorLojaAsync(Guid lojaId)
        {
            return await _dbSet
                .Where(e => e.LojaId == lojaId && e.Active)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Estoque>> GetEstoquesValidosAsync()
        {
            return await _dbSet
                .Where(e => e.ValidadeEstoq == null || e.ValidadeEstoq > DateTime.Now)
                .Where(e => e.Active)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Estoque>> GetComProdutosAsync()
        {
            return await _dbSet
                .Include(e => e.EstoqueProdutos)
                .Where(e => e.Active)
                .ToListAsync();
        }
    }
}
