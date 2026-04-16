using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class EstoqueProduto : BaseEntity
{
    public Guid EstoqueId { get; set; }

    public Guid ProdutoId { get; set; }
}
