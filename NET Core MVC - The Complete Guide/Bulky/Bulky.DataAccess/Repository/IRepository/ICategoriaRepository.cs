using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

public interface ICategoriaRepository : IRepository<Categoria>
{
    void Atualizar(Categoria entidade);
}
