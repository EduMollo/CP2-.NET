using LojaVirtual.Domain.Entities;

namespace LojaVirtual.Application.Repositories
{
    public interface ILojaRepository : IRepository<Loja>
    {
        Task<Loja?> GetByPKCnpjAsync(string cnpj);
        Task<IReadOnlyCollection<Loja>> GetComEstoquesAsync();
    }
}
