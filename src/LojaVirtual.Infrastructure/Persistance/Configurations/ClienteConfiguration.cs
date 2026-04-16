using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("TB_CLIENTE");

            builder.HasKey(c => c.Id)
                .HasName("PK_CLIENTE_ID");

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.NomeCliente)
                .HasColumnName("NOME_CLIENTE")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(c => c.TelefoneCliente)
                .HasColumnName("TELEFONE_CLIENTE")
                .HasColumnType("VARCHAR(20)")
                .IsRequired(false);

            builder.Property(c => c.CpfCliente)
                .HasColumnName("CPF_CLIENTE")
                .HasColumnType("VARCHAR(15)")
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();

            // Índices
            builder.HasIndex(c => c.CpfCliente)
                .HasDatabaseName("IX_CLIENTE_CPF")
                .IsUnique();
        }
    }
}
