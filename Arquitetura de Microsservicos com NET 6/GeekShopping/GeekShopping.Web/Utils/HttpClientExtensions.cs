using System.Text.Json;

namespace GeekShopping.Utils;

/// <summary>
/// Classe de extensão para HttpClient
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Método responsável por ler o conteúdo da resposta
    /// </summary>
    /// <typeparam name="T"> Tipo do objeto </typeparam>
    /// <param name="response"> Resposta da requisição </param>
    /// <returns> Retorna o conteúdo da resposta </returns>
    /// <exception cref="ApplicationException"> Exceção lançada caso a resposta não seja bem sucedida </exception>
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        // Verifica se a resposta foi bem sucedida
        if (!response.IsSuccessStatusCode) throw new ApplicationException($"Erro ao chamar a API: {response.ReasonPhrase}");

        // Lê o conteúdo da resposta
        var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        // Deserializa o conteúdo da resposta
        return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}