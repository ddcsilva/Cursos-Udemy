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
        try
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();

            if (produtos == null)
            {
                return NotFound("Nenhum produto encontrado");
            }

            return Ok(produtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter os produtos");
        }
    }

    [HttpGet("{id:int:min(1)}")]
    public ActionResult<Produto> ObterPorId(int id)
    {
        try
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o produto");
        }
    }

    [HttpPost]
    public ActionResult Incluir(Produto produto)
    {
        try
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao incluir o produto");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Alterar(int id, Produto produto)
    {
        try
        {
            if (id != produto.Id)
            {
                return BadRequest("Produto inválido");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar o produto");
        }
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Excluir(int id)
    {
        try
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir o produto");
        }
    }
}
