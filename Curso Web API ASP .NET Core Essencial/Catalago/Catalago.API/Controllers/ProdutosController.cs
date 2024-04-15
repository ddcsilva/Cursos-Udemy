using Catalago.Api.Context;
using Catalago.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalago.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController(CatalogoContext context) : ControllerBase
{
    private readonly CatalogoContext _context = context;

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> ObterTodos()
    {
        var produtos = _context.Produtos.ToList();

        if (produtos == null)
        {
            return NotFound("Nenhum produto encontrado");
        }

        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> ObterPorId(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound("Produto não encontrado");
        }

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Incluir(Produto produto)
    {
        if (produto == null)
        {
            return BadRequest("Produto inválido");
        }

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Alterar(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest("Id do produto não corresponde ao id da requisição");
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Excluir(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            return NotFound("Produto não encontrado");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return NoContent();
    }
}
