using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers;

/// <summary>
/// Controller para a entidade Regiao.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RegioesController : ControllerBase
{
    private readonly NZWalksDbContext _context;

    public RegioesController(NZWalksDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Action para obter todas as regiões.
    /// </summary>
    /// <returns> Lista de regiões. </returns>
    [HttpGet]
    public IActionResult ObterTodos()
    {
        // ToList() é um método de extensão que executa a query no banco de dados e retorna uma lista.
        var regioes = _context.Regioes.ToList();

        // Ok() é um método que retorna um status 200.
        return Ok(regioes);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult ObterPorId([FromRoute] Guid id) // FromRoute é um atributo que indica que o parâmetro vem da rota.
    {
        // Find() é um método do Entity Framework que busca uma entidade pelo seu ID.
        var regiao = _context.Regioes.Find(id);

        if (regiao == null)
        {
            // NotFound() é um método que retorna um status 404.
            return NotFound();
        }

        return Ok(regiao);
    }
}
