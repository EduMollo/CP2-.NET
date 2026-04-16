using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class Estoque : BaseEntity
{
    public int QntdEstoq { get; set; }

    public DateTime? ValidadeEstoq { get; set; }

    // N:1
    public Guid LojaId { get; set; }

    // N:N
    public List<EstoqueProduto> EstoqueProdutos { get; set; } = new List<EstoqueProduto>();
}
