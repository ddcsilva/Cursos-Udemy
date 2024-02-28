using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository;

/// <summary>
/// Interface responsável por definir os métodos de acesso ao banco de dados de Categoria.
/// </summary>
public interface ICategoriaRepository : IRepository<Categoria>
{
    void Atualizar(Categoria entidade);
}
