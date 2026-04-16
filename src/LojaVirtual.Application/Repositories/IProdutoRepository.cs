using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto?> GetProdutoComEstoquesAsync(Guid produtoId);
        Task<Produto?> GetProdutoComCategoriasAsync(Guid produtoId);
        Task<Produto?> GetProdutoComEstoquesECategoriasAsync(Guid produtoId);
        Task<IReadOnlyCollection<Produto>> GetProdutosPorClienteAsync(Guid clienteId);
        Task<IReadOnlyCollection<Produto>> GetProdutosPorCategoriaAsync(Guid categoriaId);
        Task<IReadOnlyCollection<Produto>> GetProdutosPorEstoqueAsync(Guid estoqueId);
    }
}
