using MagicVilla.API.Data;
using MagicVilla.API.Logging;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VilasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogging _logger;

    public VilasController(ApplicationDbContext context, ILogging logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VilaDTO>> ObterVilas()
    {
        _logger.Log("Obtendo todas as Villas", "info");
        return Ok(_context.Vilas.ToList());
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

        var vila = _context.Vilas.FirstOrDefault(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound("Vila não encontrada");
        }

        _logger.Log($"Vila encontrada: {id}", "info");
        return Ok(_context.Vilas.FirstOrDefault(v => v.Id == id));
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

        if (_context.Vilas.FirstOrDefault(v => v.Nome == vilaDTO.Nome) != null)
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

        Vila model = new()
        {
            Id = vilaDTO.Id,
            Nome = vilaDTO.Nome,
            Detalhes = vilaDTO.Detalhes,
            Avaliacao = vilaDTO.Avaliacao,
            MetrosQuadrados = vilaDTO.MetrosQuadrados,
            Quartos = vilaDTO.Quartos,
            ImagemUrl = vilaDTO.ImagemUrl,
            Comodidade = vilaDTO.Comodidade
        };

        _context.Vilas.Add(model);
        _context.SaveChanges();

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

        var vila = _context.Vilas.FirstOrDefault(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        vila.Nome = villaDTO.Nome;
        vila.Detalhes = villaDTO.Detalhes;
        vila.Avaliacao = villaDTO.Avaliacao;
        vila.MetrosQuadrados = villaDTO.MetrosQuadrados;
        vila.Quartos = villaDTO.Quartos;
        vila.ImagemUrl = villaDTO.ImagemUrl;
        vila.Comodidade = villaDTO.Comodidade;

        _context.Vilas.Update(vila);
        _context.SaveChanges();

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

        var vila = _context.Vilas.FirstOrDefault(v => v.Id == id);
        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        _context.Vilas.Remove(vila);
        _context.SaveChanges();

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

        var vila = _context.Vilas.AsNoTracking().FirstOrDefault(v => v.Id == id);

        VilaDTO vilaDTO = new VilaDTO
        {
            Id = vila.Id,
            Nome = vila.Nome,
            Detalhes = vila.Detalhes,
            Avaliacao = vila.Avaliacao,
            MetrosQuadrados = vila.MetrosQuadrados,
            Quartos = vila.Quartos,
            ImagemUrl = vila.ImagemUrl,
            Comodidade = vila.Comodidade
        };

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        patch.ApplyTo(vilaDTO, ModelState);

        Vila model = new Vila
        {
            Id = vilaDTO.Id,
            Nome = vilaDTO.Nome,
            Detalhes = vilaDTO.Detalhes,
            Avaliacao = vilaDTO.Avaliacao,
            MetrosQuadrados = vilaDTO.MetrosQuadrados,
            Quartos = vilaDTO.Quartos,
            ImagemUrl = vilaDTO.ImagemUrl,
            Comodidade = vilaDTO.Comodidade
        };

        _context.Vilas.Update(model);
        _context.SaveChanges();

        if (!ModelState.IsValid)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest(ModelState);
        }

        _logger.Log($"Vila atualizada parcialmente: {id}", "info");
        return NoContent();
    }
}
