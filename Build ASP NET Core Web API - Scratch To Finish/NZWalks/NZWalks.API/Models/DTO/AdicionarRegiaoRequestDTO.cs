using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para adicionar uma nova região ao banco de dados.
/// </summary>
public class AdicionarRegiaoRequestDTO
{
    [Required(ErrorMessage = "O código da região é obrigatório.")]
    [MinLength(3, ErrorMessage = "O código da região deve ter no mínimo 3 caracteres.")]
    [MaxLength(3, ErrorMessage = "O código da região deve ter no máximo 3 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "O nome da região é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome da região deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    public string? ImagemUrl { get; set; }
}