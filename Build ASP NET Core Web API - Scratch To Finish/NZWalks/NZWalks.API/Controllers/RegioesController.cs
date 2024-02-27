using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas às regiões na aplicação NZWalks.
/// Fornece funcionalidades para obter, adicionar, atualizar e remover regiões.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RegioesController : ControllerBase
{
    private readonly IRegiaoRepository _regiaoRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="RegioesController"/>.
    /// </summary>
    /// <param name="regiaoRepository">Repositório para operações de dados de região.</param>
    /// <param name="mapper">Ferramenta para mapeamento entre modelos de domínio e DTOs.</param>
    public RegioesController(IRegiaoRepository regiaoRepository, IMapper mapper)
    {
        _regiaoRepository = regiaoRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém uma lista de todas as regiões.
    /// </summary>
    /// <returns>Uma ação que resulta em uma resposta HTTP com a lista de regiões.</returns>
    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> ObterTodos()
    {
        var regioes = await _regiaoRepository.ObterTodosAsync();
        return Ok(_mapper.Map<List<RegiaoDTO>>(regioes));
    }

    /// <summary>
    /// Obtém uma região específica pelo seu identificador único (ID).
    /// </summary>
    /// <param name="id">O ID da região a ser obtida.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP com os detalhes da região solicitada.</returns>
    [HttpGet("{id:Guid}")]
    [Authorize]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
    {
        var regiao = await _regiaoRepository.ObterPorIdAsync(id);

        if (regiao == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<RegiaoDTO>(regiao));
    }

    /// <summary>
    /// Adiciona uma nova região.
    /// </summary>
    /// <param name="request">O DTO contendo as informações da região a ser adicionada.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP indicando o sucesso da operação.</returns>
    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarRegiaoRequestDTO request)
    {
        var regiaoModel = _mapper.Map<Regiao>(request);
        var regiaoCriada = await _regiaoRepository.AdicionarAsync(regiaoModel);
        return CreatedAtAction(nameof(ObterPorId), new { id = regiaoCriada.Id }, _mapper.Map<RegiaoDTO>(regiaoCriada));
    }

    /// <summary>
    /// Atualiza os dados de uma região existente.
    /// </summary>
    /// <param name="id">O ID da região a ser atualizada.</param>
    /// <param name="request">O DTO contendo as novas informações da região.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP indicando o sucesso da operação.</returns>
    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarRegiaoRequestDTO request)
    {
        var regiaoModel = _mapper.Map<Regiao>(request);

        regiaoModel = await _regiaoRepository.AtualizarAsync(id, regiaoModel);

        if (regiaoModel == null)
        {
            return NotFound("A região não foi encontrada.");
        }

        return Ok(_mapper.Map<RegiaoDTO>(regiaoModel));
    }

    /// <summary>
    /// Remove uma região pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da região a ser removida.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP indicando o sucesso da operação.</returns>
    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Remover([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("O ID da região é inválido.");
        }

        var regiaoRemovida = await _regiaoRepository.RemoverAsync(id);

        if (regiaoRemovida == null)
        {
            return NotFound("A região não foi encontrada.");
        }

        return NoContent();
    }
}