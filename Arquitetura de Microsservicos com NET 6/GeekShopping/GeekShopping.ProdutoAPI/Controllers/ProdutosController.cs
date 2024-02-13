using GeekShopping.ProdutoAPI.Data.ValueObjects;
using GeekShopping.ProdutoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProdutoAPI.Controllers;

/// <summary>
/// Controller de produtos
/// </summary>
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
    public async Task<ActionResult<IEnumerable<ProdutoVO>>> ObterTodos()
    {
        var produtos = await _produtoRepository.ObterTodos();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoVO>> ObterPorId(long id)
    {
        var produto = await _produtoRepository.ObterPorId(id);
        if (produto.Id <= 0) return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoVO>> Inserir(ProdutoVO produtoVO)
    {
        if (produtoVO == null) return BadRequest();
        var produtoInserido = await _produtoRepository.Inserir(produtoVO);
        return Ok(produtoInserido);
    }

    [HttpPut]
    public async Task<ActionResult<ProdutoVO>> Atualizar(ProdutoVO produtoVO)
    {
        if (produtoVO == null) return BadRequest();
        var produtoAtualizado = await _produtoRepository.Atualizar(produtoVO);
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Excluir(long id)
    {
        var status = await _produtoRepository.Excluir(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}