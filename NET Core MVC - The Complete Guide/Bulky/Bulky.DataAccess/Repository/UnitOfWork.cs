using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly BulkyContext _context;
    public ICategoriaRepository Categoria { get; private set; }
    public IProdutoRepository Produto { get; private set; }

    public UnitOfWork(BulkyContext context)
    {
        _context = context;
        Categoria = new CategoriaRepository(_context);
        Produto = new ProdutoRepository(_context);
    }

    public void Salvar()
    {
        _context.SaveChanges();
    }
}
