namespace GeekShopping.ProdutoAPI.Data.ValueObjects;

/// <summary>
/// Classe de Value Object para Produto
/// </summary>
public class ProdutoVO
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public string NomeCategoria { get; set; }
    public string ImagemUrl { get; set; }
}