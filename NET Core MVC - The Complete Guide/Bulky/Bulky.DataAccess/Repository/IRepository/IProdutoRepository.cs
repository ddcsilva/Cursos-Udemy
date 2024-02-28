using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

/// <summary>
/// Interface responsável por definir os métodos de acesso ao banco de dados de Produto.
/// </summary>
public interface IProdutoRepository : IRepository<Produto>
{
    void Atualizar(Produto entidade);
}
