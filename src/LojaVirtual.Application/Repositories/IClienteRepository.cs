using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByCpfAsync(string cpf);
        Task<Cliente?> GetClienteComProdutosAsync(Guid clienteId);
        Task<IReadOnlyCollection<Cliente>> GetClientesComProdutosAsync();
    }
}
