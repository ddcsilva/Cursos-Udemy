using System.Collections.ObjectModel;

namespace Catalago.Api.Models;

/// <summary>
/// Classe que representa uma categoria de produtos
/// </summary>
public class Categoria
{
    public Categoria()
    {
        // Inicializa a coleção de produtos para evitar null reference exception
        Produtos = new Collection<Produto>();
    }

    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    // Relacionamento 1:N (1 Categoria possui N Produtos)
    public ICollection<Produto>? Produtos { get; set; }
}