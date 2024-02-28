using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    private readonly BulkyContext _context;

    public ProdutoRepository(BulkyContext context) : base(context)
    {
        _context = context;
    }

    public void Atualizar(Produto entidade)
    {
        _context.Update(entidade);
    }
}
