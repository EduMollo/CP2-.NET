using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class CategoriaProduto : BaseEntity
{
    public Guid CategoriaId { get; set; }

    public Guid ProdutoId { get; set; }
}
