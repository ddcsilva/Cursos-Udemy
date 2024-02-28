using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

/// <summary>
/// Classe responsável por definir os métodos de acesso ao banco de dados.
/// </summary>
/// <typeparam name="T"> Tipo da entidade. </typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    // Representa o contexto do banco de dados
    private readonly BulkyContext _context;
    // Representa a tabela do banco de dados
    private DbSet<T> _dbSet;

    public Repository(BulkyContext context)
    {
        _context = context;

        // Define a tabela do banco de dados
        // Dessa forma, não é necessário criar um DbSet para cada entidade
        _dbSet = _context.Set<T>();
    }

    public void Adicionar(T entidade)
    {
        // Adiciona a entidade genérica ao DbSet
        _dbSet.Add(entidade);
    }

    public T Obter(Expression<Func<T, bool>> filtro)
    {
        // Cria uma consulta com o filtro
        // É utilizado IQueryable para que a consulta seja executada apenas quando necessário
        IQueryable<T> query = _dbSet;
        query = query.Where(filtro);

        // Retorna o primeiro resultado da consulta
        return query.FirstOrDefault();
    }

    public IEnumerable<T> ObterTodos()
    {
        // Cria uma consulta
        IQueryable<T> query = _dbSet;

        // Retorna todos os resultados da consulta
        return query.ToList();
    }

    public void Remover(T entidade)
    {
        // Remove a entidade genérica do DbSet
        _dbSet.Remove(entidade);
    }

    public void RemoverIntervalo(IEnumerable<T> entidades)
    {
        // Remove um intervalo de entidades genéricas do DbSet
        _dbSet.RemoveRange(entidades);
    }
}
