using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class CategoriaProdutoConfiguration : IEntityTypeConfiguration<CategoriaProduto>
    {
        public void Configure(EntityTypeBuilder<CategoriaProduto> builder)
        {
            builder.ToTable("TB_CATEGORIA_PRODUTO");

            // Chave composta (N:N)
            builder.HasKey(cp => new { cp.CategoriaId, cp.ProdutoId })
                .HasName("PK_CATEGORIA_PRODUTO");

            builder.Property(cp => cp.CategoriaId)
                .HasColumnName("CATEGORIA_ID")
                .IsRequired();

            builder.Property(cp => cp.ProdutoId)
                .HasColumnName("PRODUTO_ID")
                .IsRequired();

            builder.Property(cp => cp.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(cp => cp.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(cp => cp.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(cp => cp.CategoriaId)
                .HasDatabaseName("IX_CATEGORIA_PRODUTO_CATEGORIA");

            builder.HasIndex(cp => cp.ProdutoId)
                .HasDatabaseName("IX_CATEGORIA_PRODUTO_PRODUTO");

            // Relacionamentos
            builder.HasOne<Categoria>()
                .WithMany(c => c.CategoriaProdutos)
                .HasForeignKey(cp => cp.CategoriaId)
                .HasConstraintName("FK_CATEGORIA_PRODUTO_CATEGORIA")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Produto>()
                .WithMany(p => p.CategoriaProdutos)
                .HasForeignKey(cp => cp.ProdutoId)
                .HasConstraintName("FK_CATEGORIA_PRODUTO_PRODUTO")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
