using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly BulkyContext _context;

    public CategoriaRepository(BulkyContext context) : base(context)
    {
        _context = context;
    }

    public void Atualizar(Categoria entidade)
    {
        _context.Update(entidade);
    }
}