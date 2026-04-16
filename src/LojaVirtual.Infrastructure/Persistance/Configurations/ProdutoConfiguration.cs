using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("TB_PRODUTO");

            builder.HasKey(p => p.Id)
                .HasName("PK_PRODUTO_ID");

            builder.Property(p => p.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(p => p.NomeProd)
                .HasColumnName("NOME_PROD")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(p => p.PrecoProd)
                .HasColumnName("PRECO_PROD")
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

            builder.Property(p => p.ClienteId)
                .HasColumnName("CLIENTE_ID")
                .IsRequired(false); // Opcional, pois um produto pode não ter cliente associado

            builder.Property(p => p.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(p => p.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(p => p.ClienteId)
                .HasDatabaseName("IX_PRODUTO_CLIENTE");

            // Relacionamento N:1 com Cliente (Opcional)
            builder.HasOne<Cliente>()
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.ClienteId)
                .HasConstraintName("FK_PRODUTO_CLIENTE")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
