using MagicVilla.API.Data;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ILogger<VillaAPIController> _logger;

    public VillaAPIController(ILogger<VillaAPIController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> ObterVillas()
    {
        _logger.LogInformation("Obtendo todas as Villas");
        return Ok(VillaStore.Villas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> ObterVilla(int id)
    {
        if (id <= 0)
        {
            _logger.LogError($"Id inválido: {id}");
            return BadRequest();
        }

        var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(VillaStore.Villas.FirstOrDefault(v => v.Id == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDTO> AdicionarVilla([FromBody] VillaDTO villaDTO)
    {
        if (VillaStore.Villas.FirstOrDefault(v => v.Nome == villaDTO.Nome) != null)
        {
            ModelState.AddModelError("Nome", "Nome já existe");
            return BadRequest(ModelState);
        }

        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }

        if (villaDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDTO.Id = VillaStore.Villas.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VillaStore.Villas.Add(villaDTO);

        return CreatedAtAction(nameof(ObterVilla), new { id = villaDTO.Id }, villaDTO);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult AtualizarVilla(int id, [FromBody] VillaDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }

        var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == id);
        villa.Nome = villaDTO.Nome;
        villa.Quartos = villaDTO.Quartos;
        villa.Banheiros = villaDTO.Banheiros;

        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeletarVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeletarVilla(int id)
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

        VillaStore.Villas.Remove(villa);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "AtualizarParcialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarParcialVilla(int id, [FromBody] JsonPatchDocument<VillaDTO> patch)
    {
        if (patch == null || id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.Villas.FirstOrDefault(v => v.Id == id);

        if (villa == null)
        {
            return NotFound();
        }

        patch.ApplyTo(villa, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
}
