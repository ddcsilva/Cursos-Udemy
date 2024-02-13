namespace GeekShopping.Web.Models;

public class ProdutoModel
{
    public long Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public string? NomeCategoria { get; set; }
    public string? ImagemUrl { get; set; }
}