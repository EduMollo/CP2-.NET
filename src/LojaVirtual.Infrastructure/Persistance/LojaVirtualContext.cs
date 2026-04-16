
using Microsoft.EntityFrameworkCore;
using LojaVirtual.Domain.Entities;
using LojaVirtual.Infrastructure.Persistance.Configurations;

namespace LojaVirtual.Infrastructure.Persistance
{
    public class LojaVirtualContext : DbContext
    {
        public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) 
            : base(options) 
        { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstoqueProduto> EstoqueProdutos { get; set; }
        public DbSet<CategoriaProduto> CategoriaProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplicar configurações de entidades via Fluent API
            modelBuilder.ApplyConfiguration(new LojaConfiguration());
            modelBuilder.ApplyConfiguration(new EstoqueConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new EstoqueProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaProdutoConfiguration());
        }
    }
}