using Catalago.Api.Context;
using Catalago.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalago.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas a produtos
/// </summary>
[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly CatalogoContext _context;

    public ProdutosController(CatalogoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos.ToList();

        if (produtos == null)
        {
            return NotFound("Nenhum produto encontrado");
        }

        return produtos;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound("Produto não encontrado");
        }

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto == null)
        {
            return BadRequest("Produto inválido");
        }

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }
}
