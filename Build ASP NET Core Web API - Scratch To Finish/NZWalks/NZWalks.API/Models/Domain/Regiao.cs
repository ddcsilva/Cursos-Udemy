namespace NZWalks.API.Models.Domain;

/// <summary>
/// Entidade que representa uma região.
/// </summary>
public class Regiao
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
}
