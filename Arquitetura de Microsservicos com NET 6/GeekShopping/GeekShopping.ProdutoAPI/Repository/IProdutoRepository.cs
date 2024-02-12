using GeekShopping.ProdutoAPI.Data.ValueObjects;

namespace GeekShopping.ProdutoAPI.Repository;

public interface IProdutoRepository
{
    Task<IEnumerable<ProdutoVO>> BuscarTodos();
    Task<ProdutoVO> BuscarPorId(long id);
    Task<ProdutoVO> Inserir(ProdutoVO produto);
    Task<ProdutoVO> Atualizar(ProdutoVO produto);
    Task<bool> Excluir(long id);
}