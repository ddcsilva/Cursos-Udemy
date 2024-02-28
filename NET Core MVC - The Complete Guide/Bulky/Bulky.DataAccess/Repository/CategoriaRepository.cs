using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository;

/// <summary>
/// Classe que representa o repositório de Produto
/// </summary>
public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    // Representa o contexto do banco de dados
    private readonly BulkyContext _context;

    public CategoriaRepository(BulkyContext context) : base(context)
    {
        _context = context;
    }

    public void Atualizar(Categoria entidade)
    {
        // Atualiza a entidade de Categoria no DbSet
        _context.Update(entidade);
    }
}
