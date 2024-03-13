using MagicVilla.API.Data;
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
        return VillaStore.Villas;
    }

    [HttpGet("{id:int}")]
    public VillaDTO ObterVilla(int id)
    {
        return VillaStore.Villas.FirstOrDefault(v => v.Id == id);
    }
}
