using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public interface IProdutoRepository : IRepository<Produto>
{
    void Atualizar(Produto entidade);
}
