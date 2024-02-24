namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa uma região geográfica na Nova Zelândia.
/// </summary>
public class RegiaoDTO
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
}