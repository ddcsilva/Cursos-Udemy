namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa uma trilha para ser exibida na aplicação.
/// </summary>
public class TrilhaDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double DistanciaEmKm { get; set; }
    public string? ImagemUrl { get; set; }

    public RegiaoDTO Regiao { get; set; }
    public DificuldadeDTO Dificuldade { get; set; }
}