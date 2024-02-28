using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models;

/// <summary>
/// Classe que representa a tabela Categoria
/// </summary>
public class Categoria
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [MaxLength(30, ErrorMessage = "O nome da categoria deve ter no máximo 30 caracteres.")]
    [DisplayName("Categoria")]

    public string Nome { get; set; }

    [Required(ErrorMessage = "A ordem de exibição é obrigatória.")]
    [Range(1, 100, ErrorMessage = "A ordem de exibição deve ser entre 1 e 100.")]
    [DisplayName("Ordem de Exibição")]
    public int OrdemExibicao { get; set; }
}
