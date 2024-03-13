using MagicVilla.API.Data;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> ObterVillas()
    {
        return Ok(VillaStore.Villas);
    }

    [HttpGet("{id:int}")]
    public ActionResult<VillaDTO> ObterVilla(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(VillaStore.Villas.FirstOrDefault(v => v.Id == id));
    }
}
