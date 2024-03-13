using System.Linq.Expressions;
using MagicVilla.API.Models;

namespace MagicVilla.API.Repository.IRepository;

public interface IVilaRepository
{
    Task<List<Vila>> ObterTodos(Expression<Func<Vila, bool>> filtro = null);
    Task<Vila> Obter(Expression<Func<Vila, bool>> filtro = null, bool rastreado = true);
    Task Adicionar(Vila entity);
    Task Remover(Vila entity);
    Task Salvar();
}