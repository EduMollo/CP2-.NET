using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(LojaVirtualContext context) : base(context) { }

        public async Task<Produto?> GetProdutoComEstoquesAsync(Guid produtoId)
        {
            return await _dbSet
                .Include(p => p.EstoqueProdutos)
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<Produto?> GetProdutoComCategoriasAsync(Guid produtoId)
        {
            return await _dbSet
                .Include(p => p.CategoriaProdutos)
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<Produto?> GetProdutoComEstoquesECategoriasAsync(Guid produtoId)
        {
            return await _dbSet
                .Include(p => p.EstoqueProdutos)
                .Include(p => p.CategoriaProdutos)
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<IReadOnlyCollection<Produto>> GetProdutosPorClienteAsync(Guid clienteId)
        {
            return await _dbSet
                .Where(p => p.ClienteId == clienteId && p.Active)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Produto>> GetProdutosPorCategoriaAsync(Guid categoriaId)
        {
            return await _dbSet
                .Include(p => p.CategoriaProdutos)
                .Where(p => p.CategoriaProdutos.Any(cp => cp.CategoriaId == categoriaId) && p.Active)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Produto>> GetProdutosPorEstoqueAsync(Guid estoqueId)
        {
            return await _dbSet
                .Include(p => p.EstoqueProdutos)
                .Where(p => p.EstoqueProdutos.Any(ep => ep.EstoqueId == estoqueId) && p.Active)
                .ToListAsync();
        }
    }
}
