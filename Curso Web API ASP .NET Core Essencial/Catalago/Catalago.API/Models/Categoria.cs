using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalago.Api.Models;

/// <summary>
/// Classe que representa uma categoria de produtos
/// </summary>
[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        // Inicializa a coleção de produtos para evitar null reference exception
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string? ImagemUrl { get; set; }

    // Relacionamento 1:N (1 Categoria possui N Produtos)
    public ICollection<Produto>? Produtos { get; set; }
}