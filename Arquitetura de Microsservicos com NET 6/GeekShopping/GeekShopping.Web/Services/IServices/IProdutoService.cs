using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

/// <summary>
/// Interface de Produto Service
/// </summary>
public interface IProdutoService
{
    /// <summary>
    /// Método responsável por obter todos os produtos
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProdutoModel>> ObterTodosProdutos();

    /// <summary>
    /// Método responsável por obter um produto por id
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <returns>Retorna um produto</returns>
    Task<ProdutoModel> ObterProdutoPorId(long id);

    /// <summary>
    /// Método responsável por obter um produto por nome
    /// </summary>
    /// <param name="model">Modelo do produto</param>
    /// <returns>Retorna um produto</returns>
    Task<ProdutoModel> ObterProdutoPorNome(ProdutoModel model);

    /// <summary>
    /// Método responsável por criar um produto
    /// </summary>
    /// <param name="model">Modelo do produto</param>
    /// <returns>Retorna um produto</returns>
    Task<ProdutoModel> CriarProduto(ProdutoModel model);

    /// <summary>
    /// Método responsável por atualizar um produto
    /// </summary>
    /// <param name="model">Modelo do produto</param>
    /// <returns>Retorna um produto</returns>
    Task<ProdutoModel> AtualizarProduto(ProdutoModel model);

    /// <summary>
    /// Método responsável por excluir um produto
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <returns>Retorna um booleano informando se a operação foi realizada com sucesso</returns>
    Task<bool> ExcluirProduto(long id);
}