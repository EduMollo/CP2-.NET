using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public class LojaRepository : Repository<Loja>, ILojaRepository
    {
        public LojaRepository(LojaVirtualContext context) : base(context) { }

        public async Task<Loja?> GetByPKCnpjAsync(string cnpj)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.CnpjLoja == cnpj);
        }

        public async Task<IReadOnlyCollection<Loja>> GetComEstoquesAsync()
        {
            return await _dbSet
                .Include(l => l.Estoques)
                .ToListAsync();
        }
    }
}
