using MagicVilla.API.Data;
using MagicVilla.API.Logging;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VilasController(ILogging logger) : ControllerBase
{
    private readonly ILogging _logger = logger;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VilaDTO>> ObterVilas()
    {
        _logger.Log("Obtendo todas as Villas", "info");
        return Ok(VilaStore.Vilas);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VilaDTO> ObterVila(int id)
    {
        if (id <= 0)
        {
            _logger.Log($"Id inválido: {id}", "error");
            return BadRequest("Id inválido");
        }

        var vila = VilaStore.Vilas.FirstOrDefault(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound("Vila não encontrada");
        }

        _logger.Log($"Vila encontrada: {id}", "info");
        return Ok(VilaStore.Vilas.FirstOrDefault(v => v.Id == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VilaDTO> AdicionarVila([FromBody] VilaDTO vilaDTO)
    {
        if (vilaDTO == null)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest(vilaDTO);
        }

        if (VilaStore.Vilas.FirstOrDefault(v => v.Nome == vilaDTO.Nome) != null)
        {
            ModelState.AddModelError("Nome", "Nome já existe");
            _logger.Log("Nome já existe", "error");
            return BadRequest(ModelState);
        }

        if (vilaDTO.Id > 0)
        {
            ModelState.AddModelError("Id", "Id deve ser gerado pelo sistema");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        vilaDTO.Id = VilaStore.Vilas.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VilaStore.Vilas.Add(vilaDTO);

        _logger.Log($"Vila adicionada: {vilaDTO.Id}", "info");
        return CreatedAtAction(nameof(ObterVila), new { id = vilaDTO.Id }, vilaDTO);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult AtualizarVila(int id, [FromBody] VilaDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest();
        }

        var vila = VilaStore.Vilas.FirstOrDefault(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        vila.Nome = villaDTO.Nome;
        vila.Quartos = villaDTO.Quartos;
        vila.MetrosQuadrados = villaDTO.MetrosQuadrados;

        _logger.Log($"Vila atualizada: {id}", "info");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult ExcluirVila(int id)
    {
        if (id <= 0)
        {
            _logger.Log($"Id inválido: {id}", "error");
            return BadRequest();
        }

        var vila = VilaStore.Vilas.FirstOrDefault(v => v.Id == id);
        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        VilaStore.Vilas.Remove(vila);

        _logger.Log($"Vila excluída: {id}", "info");
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "AtualizarVilaParcial")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AtualizarParcialVilla(int id, [FromBody] JsonPatchDocument<VilaDTO> patch)
    {
        if (patch == null || id == 0)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest();
        }

        var vila = VilaStore.Vilas.FirstOrDefault(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        patch.ApplyTo(vila, ModelState);

        if (!ModelState.IsValid)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest(ModelState);
        }

        _logger.Log($"Vila atualizada parcialmente: {id}", "info");
        return NoContent();
    }
}
