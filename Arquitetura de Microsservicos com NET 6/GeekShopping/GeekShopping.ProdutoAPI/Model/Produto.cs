using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekShopping.ProdutoAPI.Model.Base;

namespace GeekShopping.ProdutoAPI.Model;

/// <summary>
/// Classe de Produto
/// </summary>
[Table("produto")]
public class Produto : BaseEntity
{
    [Column("nome")]
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O campo nome deve ter no máximo 150 caracteres")]
    public string? Nome { get; set; }

    [Column("preco")]
    [Required(ErrorMessage = "O campo preço é obrigatório")]
    [Range(1, 10000, ErrorMessage = "O campo preço deve estar entre 1 e 10000")]
    public decimal Preco { get; set; }

    [Column("descricao")]
    [StringLength(500, ErrorMessage = "O campo descrição deve ter no máximo 500 caracteres")]
    public string? Descricao { get; set; }

    [Column("nome_categoria")]
    [StringLength(50, ErrorMessage = "O campo nome da categoria deve ter no máximo 50 caracteres")]
    public string? NomeCategoria { get; set; }

    [Column("imagem_url")]
    [StringLength(300, ErrorMessage = "O campo imagem url deve ter no máximo 300 caracteres")]
    public string? ImagemUrl { get; set; }
}