using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository;

/// <summary>
/// Classe responsável por definir os métodos do UnitOfWork.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    // Representa o contexto do banco de dados
    private readonly BulkyContext _context;
    public ICategoriaRepository Categoria { get; private set; }
    public IProdutoRepository Produto { get; private set; }

    public UnitOfWork(BulkyContext context)
    {
        _context = context;

        // Inicializa o repositório de Categoria
        Categoria = new CategoriaRepository(_context);

        // Inicializa o repositório de Produto
        Produto = new ProdutoRepository(_context);
    }

    public void Salvar()
    {
        // Salva as alterações no banco de dados
        _context.SaveChanges();
    }
}
