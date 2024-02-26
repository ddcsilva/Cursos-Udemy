using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para atualizar uma região na Nova Zelândia.
/// </summary>
public class AtualizarRegiaoRequestDTO
{
    [Required(ErrorMessage = "O código da região é obrigatório.")]
    [MinLength(3, ErrorMessage = "O código da região deve ter no mínimo 3 caracteres.")]
    [MaxLength(3, ErrorMessage = "O código da região deve ter no máximo 3 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "O nome da região é obrigatório.")]
    public string Nome { get; set; }

    public string? ImagemUrl { get; set; }
}