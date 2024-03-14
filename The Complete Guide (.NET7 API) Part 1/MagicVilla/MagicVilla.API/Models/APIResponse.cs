using System.Net;

namespace MagicVilla.API.Models;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Sucesso { get; set; } = true;
    public List<string> MensagensDeErro { get; set; }
    public object Resultado { get; set; }
}
