using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Repositories
{
    public interface IEstoqueRepository : IRepository<Estoque>
    {
        Task<IReadOnlyCollection<Estoque>> GetEstoquesPorLojaAsync(Guid lojaId);
        Task<IReadOnlyCollection<Estoque>> GetEstoquesValidosAsync();
        Task<IReadOnlyCollection<Estoque>> GetComProdutosAsync();
    }
}
