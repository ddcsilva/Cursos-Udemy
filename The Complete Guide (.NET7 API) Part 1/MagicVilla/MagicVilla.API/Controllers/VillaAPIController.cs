using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> ObterVillas()
    {
        return
        [
            new VillaDTO { Id = 1, Nome = "Villa 1" },
            new VillaDTO { Id = 2, Nome = "Villa 2" },
            new VillaDTO { Id = 3, Nome = "Villa 3" }
        ];
    }
}
