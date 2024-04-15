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
        var categorias = _context.Categorias.ToList();

        if (categorias == null)
        {
            return NotFound("Nenhuma categoria encontrada");
        }

        return Ok(categorias);
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> ObterTodosComProdutos()
    {
        return Ok(_context.Categorias.Include(c => c.Produtos).ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> Obter(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada");
        }

        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Incluir(Categoria categoria)
    {
        if (categoria == null)
        {
            return BadRequest("Categoria inválida");
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return Ok("Categoria adicionada com sucesso");
    }

    [HttpPut("{id:int}")]
    public ActionResult Alterar(int id, Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return BadRequest("Id da categoria não corresponde ao id informado");
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok("Categoria atualizada com sucesso");
    }

    [HttpDelete("{id:int}")]
    public ActionResult Excluir(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok("Categoria excluída com sucesso");
    }
}