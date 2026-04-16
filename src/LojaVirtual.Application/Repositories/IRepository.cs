using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Application.Repositories
{
    /// <summary>
    /// Interface genérica de repositório para acesso a dados.
    /// </summary>
    /// <typeparam name="T">Tipo de entidade</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllActiveAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
