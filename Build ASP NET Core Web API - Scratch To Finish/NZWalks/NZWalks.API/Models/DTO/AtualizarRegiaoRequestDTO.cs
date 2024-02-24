namespace NZWalks.API.Models.DTO;

/// <summary>
/// Representa os dados necessários para atualizar uma região na Nova Zelândia.
/// </summary>
public class AtualizarRegiaoRequestDTO
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string? ImagemUrl { get; set; }
}