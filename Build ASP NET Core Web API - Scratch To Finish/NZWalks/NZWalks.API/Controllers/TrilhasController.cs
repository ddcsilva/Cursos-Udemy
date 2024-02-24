using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrilhasController : ControllerBase
{
    // ADICIONAR Trilha
    // Post: /api/trilhas
    [HttpPost]
    public async Task<ActionResult> Adicionar()
    {
        return Ok();
    }
}