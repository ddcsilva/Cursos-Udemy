using AutoMapper;
using MagicVilla.API.Logging;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using MagicVilla.API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VilasController : ControllerBase
{
    private readonly IVilaRepository _vilaRepository;
    private readonly ILogging _logger;
    private readonly IMapper _mapper;

    public VilasController(IVilaRepository vilaRepository, ILogging logger, IMapper mapper)
    {
        _vilaRepository = vilaRepository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VilaDTO>>> ObterVilas()
    {
        IEnumerable<Vila> vilas = await _vilaRepository.ObterTodos();

        _logger.Log("Obtendo todas as Villas", "info");
        return Ok(_mapper.Map<IEnumerable<VilaDTO>>(vilas));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VilaDTO>> ObterVila(int id)
    {
        if (id == 0)
        {
            _logger.Log("Id inválido", "error");
            return BadRequest();
        }

        var vila = await _vilaRepository.Obter(v => v.Id == id);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        _logger.Log($"Obtendo Vila: {id}", "info");
        return Ok(_mapper.Map<VilaDTO>(vila));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VilaDTO>> AdicionarVila([FromBody] CriarVilaDTO criarVilaDTO)
    {
        if (criarVilaDTO == null)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest(criarVilaDTO);
        }

        if (await _vilaRepository.Obter(v => v.Nome == criarVilaDTO.Nome) != null)
        {
            ModelState.AddModelError("Nome", "Nome já existe");
            _logger.Log("Nome já existe", "error");
            return BadRequest(ModelState);
        }

        Vila model = _mapper.Map<Vila>(criarVilaDTO);

        await _vilaRepository.Adicionar(model);

        _logger.Log($"Vila adicionada: {model.Id}", "info");
        return CreatedAtAction(nameof(ObterVila), new { id = model.Id }, model);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AtualizarVila(int id, [FromBody] AtualizarVilaDTO atualizarVilaDTO)
    {
        if (atualizarVilaDTO == null || id != atualizarVilaDTO.Id)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest();
        }

        Vila model = _mapper.Map<Vila>(atualizarVilaDTO);

        await _vilaRepository.Atualizar(model);

        _logger.Log($"Vila atualizada: {id}", "info");
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ExcluirVila(int id)
    {
        if (id <= 0)
        {
            _logger.Log($"Id inválido: {id}", "error");
            return BadRequest();
        }

        var vila = await _vilaRepository.Obter(v => v.Id == id);
        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        await _vilaRepository.Remover(vila);

        _logger.Log($"Vila excluída: {id}", "info");
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "AtualizarVilaParcial")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarParcialVilla(int id, [FromBody] JsonPatchDocument<AtualizarVilaDTO> patch)
    {
        if (patch == null || id == 0)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest();
        }

        var vila = await _vilaRepository.Obter(v => v.Id == id, false);

        AtualizarVilaDTO vilaDTO = _mapper.Map<AtualizarVilaDTO>(vila);

        if (vila == null)
        {
            _logger.Log($"Vila não encontrada: {id}", "error");
            return NotFound();
        }

        patch.ApplyTo(vilaDTO, ModelState);

        Vila model = _mapper.Map<Vila>(vilaDTO);

        await _vilaRepository.Atualizar(model);

        if (!ModelState.IsValid)
        {
            _logger.Log("Vila inválida", "error");
            return BadRequest(ModelState);
        }

        _logger.Log($"Vila atualizada parcialmente: {id}", "info");
        return NoContent();
    }
}
