using MagicVilla.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla.API.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    internal DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Adicionar(T entity)
    {
        await _dbSet.AddAsync(entity);
        await Salvar();
    }

    public async Task<T> Obter(Expression<Func<T, bool>> filtro = null, bool rastreado = true)
    {
        IQueryable<T> query = _dbSet;

        if (filtro != null)
        {
            query = query.Where(filtro);
        }

        if (!rastreado)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> ObterTodos(Expression<Func<T, bool>> filtro = null)
    {
        IQueryable<T> query = _dbSet;

        if (filtro != null)
        {
            query = query.Where(filtro);
        }

        return await query.ToListAsync();
    }

    public async Task Remover(T entity)
    {
        _dbSet.Remove(entity);
        await Salvar();
    }

    public async Task Salvar()
    {
        await _context.SaveChangesAsync();
    }
}
