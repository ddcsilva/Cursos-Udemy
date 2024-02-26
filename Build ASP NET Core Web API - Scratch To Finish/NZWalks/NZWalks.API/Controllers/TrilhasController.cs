using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas às trilhas na aplicação NZWalks.
/// Fornece funcionalidades para obter, adicionar, atualizar e remover trilhas.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TrilhasController : ControllerBase
{
    private readonly ITrilhaRepository _trilhaRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="TrilhasController"/>.
    /// </summary>
    /// <param name="trilhaRepository">Repositório para operações de dados de trilha.</param>
    /// <param name="mapper">Ferramenta para mapeamento entre modelos de domínio e DTOs.</param>
    public TrilhasController(ITrilhaRepository trilhaRepository, IMapper mapper)
    {
        _trilhaRepository = trilhaRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém uma lista de todas as trilhas.
    /// </summary>
    /// <returns>Uma ação que resulta em uma resposta HTTP com a lista de trilhas.</returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var trilhas = await _trilhaRepository.ObterTodosAsync();
        return Ok(_mapper.Map<List<TrilhaDTO>>(trilhas));
    }

    /// <summary>
    /// Obtém uma trilha específica pelo seu identificador único (ID).
    /// </summary>
    /// <param name="id">O ID da trilha a ser obtida.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP com os detalhes da trilha solicitada.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
    {
        var trilha = await _trilhaRepository.ObterPorIdAsync(id);

        if (trilha == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TrilhaDTO>(trilha));
    }

    /// <summary>
    /// Adiciona uma nova trilha ao banco de dados.
    /// </summary>
    /// <param name="request">Os dados da trilha a ser adicionada.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP com a trilha adicionada.</returns>
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarTrilhaRequestDTO request)
    {
        var trilhaModel = _mapper.Map<Trilha>(request);
        var trilhaCriada = await _trilhaRepository.AdicionarAsync(trilhaModel);
        return CreatedAtAction(nameof(ObterPorId), new { id = trilhaCriada.Id }, _mapper.Map<TrilhaDTO>(trilhaCriada));
    }

    /// <summary>
    /// Atualiza os dados de uma trilha existente no banco de dados.
    /// </summary>
    /// <param name="id">O ID da trilha a ser atualizada.</param>
    /// <param name="request">Os novos dados da trilha.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP com a trilha atualizada.</returns>
    [HttpPut("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarTrilhaRequestDTO request)
    {
        var trilhaModel = _mapper.Map<Trilha>(request);
        var trilhaAtualizada = await _trilhaRepository.AtualizarAsync(id, trilhaModel);

        if (trilhaAtualizada == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<TrilhaDTO>(trilhaAtualizada));
    }

    /// <summary>
    /// Remove uma trilha do banco de dados.
    /// </summary>
    /// <param name="id">O ID da trilha a ser removida.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP sem conteúdo.</returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remover([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        var trilhaRemovida = await _trilhaRepository.RemoverAsync(id);

        if (trilhaRemovida == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}