using System.Linq.Expressions;
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Repository;

public class VilaRepository : IVilaRepository
{
    private readonly ApplicationDbContext _context;

    public VilaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Vila entity)
    {
        await _context.Vilas.AddAsync(entity);
        await Salvar();
    }

    public async Task Atualizar(Vila entity)
    {
        _context.Vilas.Update(entity);
        await Salvar();
    }

    public async Task<Vila> Obter(Expression<Func<Vila, bool>> filtro = null, bool rastreado = true)
    {
        IQueryable<Vila> query = _context.Vilas;

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

    public async Task<List<Vila>> ObterTodos(Expression<Func<Vila, bool>> filtro = null)
    {
        IQueryable<Vila> query = _context.Vilas;

        if (filtro != null)
        {
            query = query.Where(filtro);
        }

        return await query.ToListAsync();
    }

    public async Task Remover(Vila entity)
    {
        _context.Vilas.Remove(entity);
        await Salvar();
    }

    public async Task Salvar()
    {
        await _context.SaveChangesAsync();
    }
}