using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

/// <summary>
/// Classe que representa o repositório de Produto
/// </summary>
public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    // Representa o contexto do banco de dados
    private readonly BulkyContext _context;

    public ProdutoRepository(BulkyContext context) : base(context)
    {
        _context = context;
    }

    public void Atualizar(Produto entidade)
    {
        // Atualiza a entidade de Produto no DbSet
        _context.Update(entidade);
    }
}
