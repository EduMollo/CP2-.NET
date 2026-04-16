using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class LojaConfiguration : IEntityTypeConfiguration<Loja>
    {
        public void Configure(EntityTypeBuilder<Loja> builder)
        {
            builder.ToTable("TB_LOJA");

            builder.HasKey(l => l.Id)
                .HasName("PK_LOJA_ID");

            builder.Property(l => l.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(l => l.NomeLoja)
                .HasColumnName("NOME_LOJA")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(l => l.CnpjLoja)
                .HasColumnName("CNPJ_LOJA")
                .HasColumnType("VARCHAR(18)")
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(l => l.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(l => l.CnpjLoja)
                .HasDatabaseName("IX_LOJA_CNPJ")
                .IsUnique();

            // Relacionamentos
            builder.HasMany(l => l.Estoques)
                .WithOne()
                .HasForeignKey(e => e.LojaId)
                .HasConstraintName("FK_ESTOQUE_LOJA")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
