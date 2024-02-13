using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoModel>> ObterTodosProdutos();
    Task<ProdutoModel> ObterProdutoPorId(long id);
    Task<ProdutoModel> ObterProdutoPorNome(ProdutoModel model);
    Task<ProdutoModel> CriarProduto(ProdutoModel model);
    Task<ProdutoModel> AtualizarProduto(ProdutoModel model);
    Task<bool> ExcluirProduto(long id);
}