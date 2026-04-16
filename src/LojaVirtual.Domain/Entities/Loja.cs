using LojaVirtual.Domain.Commons;

namespace LojaVirtual.Domain.Entities;

public class Loja : BaseEntity
{
    public string NomeLoja { get; set; } = string.Empty;

    public string CnpjLoja { get; set; } = string.Empty;

    // 1:N
    public List<Estoque> Estoques { get; set; } = new List<Estoque>();
}
