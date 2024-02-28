using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository;

/// <summary>
/// Interface responsável por definir os métodos de acesso ao banco de dados.
/// </summary>
/// <typeparam name="T"> Tipo da entidade. </typeparam>
public interface IRepository<T> where T : class
{
    IEnumerable<T> ObterTodos();
    T Obter(Expression<Func<T, bool>> filtro);
    void Adicionar(T entidade);
    void Remover(T entidade);
    void RemoverIntervalo(IEnumerable<T> entidades);
}