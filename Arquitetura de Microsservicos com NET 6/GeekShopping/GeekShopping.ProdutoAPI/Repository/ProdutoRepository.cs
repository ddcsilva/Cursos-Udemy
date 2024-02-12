using AutoMapper;
using GeekShopping.ProdutoAPI.Data.ValueObjects;
using GeekShopping.ProdutoAPI.Model;
using GeekShopping.ProdutoAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProdutoAPI.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly MySqlContext _context;
    private readonly IMapper _mapper;

    public ProdutoRepository(MySqlContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoVO>> BuscarTodos()
    {
        List<Produto> produtos = await _context.Produtos.ToListAsync();
        return _mapper.Map<List<ProdutoVO>>(produtos);
    }

    public async Task<ProdutoVO> BuscarPorId(long id)
    {
        Produto produto = await _context.Produtos.FindAsync(id);
        return _mapper.Map<ProdutoVO>(produto);
    }

    public async Task<ProdutoVO> Inserir(ProdutoVO produtoVO)
    {
        Produto produto = _mapper.Map<Produto>(produtoVO);
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProdutoVO>(produto);
    }

    public async Task<ProdutoVO> Atualizar(ProdutoVO produtoVO)
    {
        Produto produto = _mapper.Map<Produto>(produtoVO);
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProdutoVO>(produto);
    }

    public async Task<bool> Excluir(long id)
    {
        try
        {
            Produto produto = await _context.Produtos.FindAsync(id);

            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}