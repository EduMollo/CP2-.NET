using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(LojaVirtualContext context) : base(context) { }

        public async Task<Cliente?> GetByCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CpfCliente == cpf && c.Active);
        }

        public async Task<Cliente?> GetClienteComProdutosAsync(Guid clienteId)
        {
            return await _dbSet
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == clienteId);
        }

        public async Task<IReadOnlyCollection<Cliente>> GetClientesComProdutosAsync()
        {
            return await _dbSet
                .Include(c => c.Produtos)
                .Where(c => c.Active)
                .ToListAsync();
        }
    }
}
