using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class Produto : BaseEntity
{
    public string NomeProd { get; set; } = string.Empty;

    public decimal PrecoProd { get; set; }

    // N:1
    public Guid? ClienteId { get; set; }

    // N:N
    public List<EstoqueProduto> EstoqueProdutos { get; set; } = new List<EstoqueProduto>();

    // N:N
    public List<CategoriaProduto> CategoriaProdutos { get; set; } = new List<CategoriaProduto>();
}
