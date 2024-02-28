namespace Bulky.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoriaRepository Categoria { get; }
    IProdutoRepository Produto { get; }

    void Salvar();
}
