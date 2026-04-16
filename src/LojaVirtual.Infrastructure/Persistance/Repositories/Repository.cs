using LojaVirtual.Application.Repositories;
using LojaVirtual.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Persistance.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly LojaVirtualContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(LojaVirtualContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllActiveAsync()
        {
            return await _dbSet.Where(e => e.Active).ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            await Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
