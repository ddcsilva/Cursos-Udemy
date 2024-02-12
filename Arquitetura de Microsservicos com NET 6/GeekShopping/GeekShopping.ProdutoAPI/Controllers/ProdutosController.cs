using GeekShopping.ProdutoAPI.Data.ValueObjects;
using GeekShopping.ProdutoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProdutoAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoVO>>> BuscarTodos()
    {
        var produtos = await _produtoRepository.BuscarTodos();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoVO>> BuscarPorId(long id)
    {
        var produto = await _produtoRepository.BuscarPorId(id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }
}