using GeekShopping.ProdutoAPI.Data.ValueObjects;

namespace GeekShopping.ProdutoAPI.Repository;

/// <summary>
/// Interface de Produto Repository
/// </summary>
public interface IProdutoRepository
{
    /// <summary>
    /// Método responsável por obter todos os produtos
    /// </summary>
    /// <returns>Retorna uma lista de produtos</returns>
    Task<IEnumerable<ProdutoVO>> ObterTodos();

    /// <summary>
    /// Método responsável por obter um produto por id
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <returns>Retorna um produto</returns>
    Task<ProdutoVO> ObterPorId(long id);

    /// <summary>
    /// Método responsável por inserir um produto
    /// </summary>
    /// <param name="produto">Produto a ser inserido</param>
    /// <returns>Retorna o produto inserido</returns>
    Task<ProdutoVO> Inserir(ProdutoVO produtoVO);

    /// <summary>
    /// Método responsável por atualizar um produto
    /// </summary>
    /// <param name="produto">Produto a ser atualizado</param>
    /// <returns>Retorna o produto atualizado</returns>
    Task<ProdutoVO> Atualizar(ProdutoVO produtoVO);

    /// <summary>
    /// Método responsável por excluir um produto
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <returns>Retorna um booleano informando se a operação foi realizada com sucesso</returns>
    Task<bool> Excluir(long id);
}