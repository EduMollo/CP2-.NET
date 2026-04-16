using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria?> GetCategoriaComProdutosAsync(Guid categoriaId);
        Task<IReadOnlyCollection<Categoria>> GetCategoriasComProdutosAsync();
    }
}
