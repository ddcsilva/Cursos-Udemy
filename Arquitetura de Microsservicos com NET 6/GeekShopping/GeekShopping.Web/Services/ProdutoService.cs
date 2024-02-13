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
        throw new NotImplementedException();
    }

    public async Task<ProdutoModel> CriarProduto(ProdutoModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<ProdutoModel> AtualizarProduto(ProdutoModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExcluirProduto(long id)
    {
        throw new NotImplementedException();
    }
}
