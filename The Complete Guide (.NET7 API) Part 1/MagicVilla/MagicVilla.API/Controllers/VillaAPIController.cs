using MagicVilla.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Villa> ObterVillas()
    {
        return
        [
            new Villa { Id = 1, Nome = "Villa 1" },
            new Villa { Id = 2, Nome = "Villa 2" },
            new Villa { Id = 3, Nome = "Villa 3" }
        ];
    }
}
