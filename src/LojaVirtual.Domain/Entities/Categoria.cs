using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class Categoria : BaseEntity
{
    public string NomeCategoria { get; set; } = string.Empty;

    public string? DescCategoria { get; set; }

    // N:N
    public List<CategoriaProduto> CategoriaProdutos { get; set; } = new List<CategoriaProduto>();
}
