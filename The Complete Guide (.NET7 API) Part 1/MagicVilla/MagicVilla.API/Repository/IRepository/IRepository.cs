using MagicVilla.API.Models;
using System.Linq.Expressions;

namespace MagicVilla.API.Repository;

public interface IRepository<T> where T : class
{
    Task<T> Obter(Expression<Func<T, bool>> filtro = null, bool rastreado = true);
    Task<List<T>> ObterTodos(Expression<Func<T, bool>> filtro = null);
    Task Adicionar(T entity);
    Task Remover(T entity);
    Task Salvar();
}
