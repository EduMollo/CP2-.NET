using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class EstoqueProdutoConfiguration : IEntityTypeConfiguration<EstoqueProduto>
    {
        public void Configure(EntityTypeBuilder<EstoqueProduto> builder)
        {
            builder.ToTable("TB_ESTOQUE_PRODUTO");

            // Chave composta (N:N)
            builder.HasKey(ep => new { ep.EstoqueId, ep.ProdutoId })
                .HasName("PK_ESTOQUE_PRODUTO");

            builder.Property(ep => ep.EstoqueId)
                .HasColumnName("ESTOQUE_ID")
                .IsRequired();

            builder.Property(ep => ep.ProdutoId)
                .HasColumnName("PRODUTO_ID")
                .IsRequired();

            builder.Property(ep => ep.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(ep => ep.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(ep => ep.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(ep => ep.EstoqueId)
                .HasDatabaseName("IX_ESTOQUE_PRODUTO_ESTOQUE");

            builder.HasIndex(ep => ep.ProdutoId)
                .HasDatabaseName("IX_ESTOQUE_PRODUTO_PRODUTO");

            // Relacionamentos
            builder.HasOne<Estoque>()
                .WithMany(e => e.EstoqueProdutos)
                .HasForeignKey(ep => ep.EstoqueId)
                .HasConstraintName("FK_ESTOQUE_PRODUTO_ESTOQUE")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Produto>()
                .WithMany(p => p.EstoqueProdutos)
                .HasForeignKey(ep => ep.ProdutoId)
                .HasConstraintName("FK_ESTOQUE_PRODUTO_PRODUTO")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
