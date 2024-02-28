using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> ObterTodos();
    T Obter(Expression<Func<T, bool>> filtro);
    void Adicionar(T entidade);
    void Remover(T entidade);
    void RemoverIntervalo(IEnumerable<T> entidades);
}