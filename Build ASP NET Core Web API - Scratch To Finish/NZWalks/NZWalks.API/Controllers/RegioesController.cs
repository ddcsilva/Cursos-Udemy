using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegioesController : ControllerBase
{
    private readonly NZWalksDbContext _context;

    public RegioesController(NZWalksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ObterTodos()
    {
        var regioes = _context.Regioes.ToList();

        return Ok(regioes);
    }
}
