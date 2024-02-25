namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para adicionar uma nova região ao banco de dados.
/// </summary>
public class AdicionarRegiaoRequestDTO
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
}