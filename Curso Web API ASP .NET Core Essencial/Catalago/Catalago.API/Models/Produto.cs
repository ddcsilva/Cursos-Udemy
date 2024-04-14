using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalago.Api.Models;

/// <summary>
/// Classe que representa um produto
/// </summary>
[Table("Produtos")]
public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(80, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(300, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
    public string? ImagemUrl { get; set; }

    public float? Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    // Relacionamento N:1 (N Produtos pertencem a 1 Categoria)
    public int CategoriaId { get; set; }

    // Propriedade de navegação.
    public Categoria? Categoria { get; set; }
}