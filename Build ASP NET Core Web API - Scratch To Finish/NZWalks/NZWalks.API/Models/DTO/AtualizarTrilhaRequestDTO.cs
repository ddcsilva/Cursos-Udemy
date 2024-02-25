namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para adicionar uma nova trilha ao banco de dados.
/// </summary>
public class AtualizarTrilhaRequestDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double DistanciaEmKm { get; set; }
    public string? ImagemUrl { get; set; }
    public Guid DificuldadeId { get; set; }
    public Guid RegiaoId { get; set; }
}