using System.Linq.Expressions;
using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.IRepository;

namespace MagicVilla.API.Repository;

public class VilaRepository : Repository<Vila>, IVilaRepository
{
    private readonly ApplicationDbContext _context;

    public VilaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Vila> Atualizar(Vila entity)
    {
        entity.DataAtualizacao = DateTime.Now;
        _context.Vilas.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}