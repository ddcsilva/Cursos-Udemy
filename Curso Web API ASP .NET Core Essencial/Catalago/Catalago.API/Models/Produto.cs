namespace Catalago.Api.Models;

/// <summary>
/// Classe que representa um produto
/// </summary>
public class Produto
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float? Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    // Relacionamento N:1 (N Produtos pertencem a 1 Categoria)
    public int CategoriaId { get; set; }

    // Propriedade de navegação.
    public Categoria? Categoria { get; set; }
}