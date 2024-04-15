using Catalago.Api.Context;
using Catalago.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalago.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController(CatalogoContext context) : ControllerBase
{
    private readonly CatalogoContext _context = context;

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> ObterTodos()
    {
        try
        {
            var categorias = _context.Categorias.AsNoTracking().ToList();

            if (categorias == null)
            {
                return NotFound("Nenhuma categoria encontrada");
            }

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter as categorias");
        }
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> ObterTodosComProdutos()
    {
        try
        {
            var categorias = _context.Categorias.Include(c => c.Produtos).AsNoTracking().ToList();

            if (categorias == null)
            {
                return NotFound("Nenhuma categoria encontrada");
            }

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter as categorias");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> Obter(int id)
    {
        try
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter a categoria");
        }
    }

    [HttpPost]
    public ActionResult Incluir(Categoria categoria)
    {
        try
        {
            if (categoria == null)
            {
                return BadRequest("Categoria inválida");
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Obter), new { id = categoria.Id }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao incluir a categoria");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Alterar(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.Id)
            {
                return BadRequest("Id da categoria não corresponde ao id da requisição");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar a categoria");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Excluir(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok("Categoria excluída");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao excluir a categoria");
        }
    }
}