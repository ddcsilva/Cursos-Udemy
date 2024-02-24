namespace NZWalks.API.Models.Domain;

/// <summary>
/// Representa uma região geográfica na Nova Zelândia, usada para categorizar trilhas com base em sua localização.
/// A classificação das trilhas por região facilita aos usuários a busca e seleção de trilhas em áreas específicas de interesse na Nova Zelândia.
/// </summary>
public class Regiao
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
}
