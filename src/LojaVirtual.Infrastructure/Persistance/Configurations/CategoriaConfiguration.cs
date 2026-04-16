using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.Persistance.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("TB_CATEGORIA");

            builder.HasKey(c => c.Id)
                .HasName("PK_CATEGORIA_ID");

            builder.Property(c => c.Id)
                .HasColumnName("ID")
                .IsRequired();

            builder.Property(c => c.NomeCategoria)
                .HasColumnName("NOME_CATEGORIA")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.Property(c => c.DescCategoria)
                .HasColumnName("DESC_CATEGORIA")
                .HasColumnType("VARCHAR(1000)")
                .IsRequired(false);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CRIADO_EM")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired();

            builder.Property(c => c.Active)
                .HasColumnName("ATIVO")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
