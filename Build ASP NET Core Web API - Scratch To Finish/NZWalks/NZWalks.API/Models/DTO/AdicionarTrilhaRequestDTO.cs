using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para adicionar uma nova trilha ao banco de dados.
/// </summary>
public class AdicionarTrilhaRequestDTO
{
    [Required(ErrorMessage = "O nome da trilha é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome da trilha deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição da trilha é obrigatória.")]
    [MaxLength(1000, ErrorMessage = "A descrição da trilha deve ter no máximo 1000 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "A distância da trilha é obrigatória.")]
    [Range(0, 50, ErrorMessage = "A distância da trilha deve estar entre 0 e 50 km.")]

    public double DistanciaEmKm { get; set; }

    public string? ImagemUrl { get; set; }

    [Required(ErrorMessage = "A dificuldade da trilha é obrigatória.")]
    public Guid DificuldadeId { get; set; }

    [Required(ErrorMessage = "A região da trilha é obrigatória.")]
    public Guid RegiaoId { get; set; }
}