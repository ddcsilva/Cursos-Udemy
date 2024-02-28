namespace Bulky.Models;

/// <summary>
/// Classe que representa o modelo de erro
/// </summary>
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
