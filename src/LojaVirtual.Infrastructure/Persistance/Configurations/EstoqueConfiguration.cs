using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("TB_ESTOQUE");

            builder.HasKey(e => e.Id)
                .HasName("PK_ESTOQUE_ID");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(e => e.QntdEstoq)
                .HasColumnName("QNTD_ESTOQ")
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(e => e.ValidadeEstoq)
                .HasColumnName("VALIDADE_ESTOQ")
                .HasColumnType("DATE")
                .IsRequired(false);

            builder.Property(e => e.LojaId)
                .HasColumnName("LOJA_ID")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(e => e.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(e => e.LojaId)
                .HasDatabaseName("IX_ESTOQUE_LOJA");
        }
    }
}
