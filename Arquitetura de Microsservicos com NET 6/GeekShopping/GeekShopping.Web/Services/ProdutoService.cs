using GeekShopping.Utils;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web.Services;

/// <summary>
/// Classe de serviço para Produto
/// </summary>
public class ProdutoService : IProdutoService
{
    private readonly HttpClient _client;
    public const string BasePath = "api/v1/produtos";

    public ProdutoService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ProdutoModel>> ObterTodosProdutos()
    {
        // Faz a requisição GET para a API de produtos
        var response = await _client.GetAsync(BasePath);
        // Deserializa o conteúdo da resposta para uma lista de produtos
        return await response.ReadContentAs<List<ProdutoModel>>();
    }

    public async Task<ProdutoModel> ObterProdutoPorId(long id)
    {
        // Faz a requisição GET para a API de produtos com o id do produto
        var response = await _client.GetAsync($"{BasePath}/{id}");
        // Deserializa o conteúdo da resposta para um produto
        return await response.ReadContentAs<ProdutoModel>();
    }

    public async Task<ProdutoModel> ObterProdutoPorNome(ProdutoModel model)
    {
        // Faz a requisição GET para a API de produtos com o nome do produto
        var response = await _client.GetAsync($"{BasePath}/nome/{model.Nome}");
        // Deserializa o conteúdo da resposta para um produto
        return await response.ReadContentAs<ProdutoModel>();
    }

    public async Task<ProdutoModel> CriarProduto(ProdutoModel model)
    {
        // Faz a requisição POST para a API de produtos com o modelo do produto
        var response = await _client.PostAsJsonAsync(BasePath, model);

        if (!response.IsSuccessStatusCode)
        {
            // Lança uma exceção se o status da resposta indicar uma falha.
            throw new ApplicationException($"Erro ao chamar a API: {response.ReasonPhrase}");
        }

        // Deserializa o conteúdo da resposta para um produto
        return await response.ReadContentAs<ProdutoModel>();
    }

    public async Task<ProdutoModel> AtualizarProduto(ProdutoModel model)
    {
        // Faz a requisição PUT para a API de produtos com o modelo do produto
        var response = await _client.PutAsJsonAsync($"{BasePath}/{model.Id}", model);

        if (!response.IsSuccessStatusCode)
        {
            // Lança uma exceção se o status da resposta indicar uma falha.
            throw new ApplicationException($"Erro ao chamar a API: {response.ReasonPhrase}");
        }

        // Deserializa o conteúdo da resposta para um produto
        return await response.ReadContentAs<ProdutoModel>();
    }

    public async Task<bool> ExcluirProduto(long id)
    {
        // Faz a requisição DELETE para a API de produtos com o id do produto
        var response = await _client.DeleteAsync($"{BasePath}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            // Lança uma exceção se o status da resposta indicar uma falha.
            throw new ApplicationException($"Erro ao chamar a API: {response.ReasonPhrase}");
        }

        // Deserializa o conteúdo da resposta para um booleano
        return await response.ReadContentAs<bool>();
    }
}
