using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Utils;

/// <summary>
/// Classe de extensão para adicionar funcionalidades ao HttpClient, facilitando operações com JSON.
/// </summary>
public static class HttpClientExtensions
{
    // Define o tipo de conteúdo padrão para as requisições como 'application/json'.
    private static readonly MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

    /// <summary>
    /// Deserializa o conteúdo da resposta de uma requisição HTTP para o tipo especificado.
    /// </summary>
    /// <typeparam name="T">O tipo para o qual o conteúdo da resposta será deserializado.</typeparam>
    /// <param name="response">A resposta da requisição HTTP.</param>
    /// <returns>O conteúdo da resposta deserializado para o tipo especificado.</returns>
    /// <exception cref="ApplicationException">Lançada se a resposta não for bem-sucedida ou a deserialização falhar.</exception>
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            // Lança uma exceção se o status da resposta indicar uma falha.
            throw new ApplicationException($"Erro ao chamar a API: {response.ReasonPhrase}");
        }

        var dados = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var resultado = JsonSerializer.Deserialize<T>(dados, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (resultado == null)
        {
            // Lança uma exceção se a deserialização resultar em um objeto nulo.
            throw new ApplicationException("Falha ao deserializar a resposta da API.");
        }

        return resultado;
    }

    /// <summary>
    /// Envia uma requisição POST com o corpo em formato JSON.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto que será serializado para JSON e enviado no corpo da requisição.</typeparam>
    /// <param name="httpClient">O cliente HTTP que fará a requisição.</param>
    /// <param name="url">A URL para a qual a requisição será enviada.</param>
    /// <param name="dados">O objeto a ser serializado para JSON e enviado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a resposta da requisição HTTP.</returns>
    public static Task<HttpResponseMessage> PostarComoJsonAsync<T>(this HttpClient httpClient, string url, T dados)
    {
        var dadosString = JsonSerializer.Serialize(dados);
        var conteudo = new StringContent(dadosString);
        conteudo.Headers.ContentType = contentType;
        return httpClient.PostAsync(url, conteudo);
    }

    /// <summary>
    /// Envia uma requisição PUT com o corpo em formato JSON.
    /// </summary>
    /// <typeparam name="T">O tipo do objeto que será serializado para JSON e enviado no corpo da requisição.</typeparam>
    /// <param name="httpClient">O cliente HTTP que fará a requisição.</param>
    /// <param name="url">A URL para a qual a requisição será enviada.</param>
    /// <param name="dados">O objeto a ser serializado para JSON e enviado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo a resposta da requisição HTTP.</returns>
    public static Task<HttpResponseMessage> AtualizarComoJsonAsync<T>(this HttpClient httpClient, string url, T dados)
    {
        var dadosString = JsonSerializer.Serialize(dados);
        var conteudo = new StringContent(dadosString);
        conteudo.Headers.ContentType = contentType;
        return httpClient.PutAsync(url, conteudo);
    }
}