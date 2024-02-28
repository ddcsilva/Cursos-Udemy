using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BulkyContext _context;
    private DbSet<T> _dbSet;

    public Repository(BulkyContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> ObterTodos()
    {
        IQueryable<T> query = _dbSet;
        return query.ToList();
    }

    public T Obter(Expression<Func<T, bool>> filtro)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(filtro);
        return query.FirstOrDefault();
    }

    public void Adicionar(T entidade)
    {
        _dbSet.Add(entidade);
    }

    public void Remover(T entidade)
    {
        _dbSet.Remove(entidade);
    }

    public void RemoverIntervalo(IEnumerable<T> entidades)
    {
        _dbSet.RemoveRange(entidades);
    }
}
