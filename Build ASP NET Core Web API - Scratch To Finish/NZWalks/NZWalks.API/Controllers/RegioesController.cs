using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

/*
    Métodos Assíncronos:
    A vantagem de usar async/await é que a thread do servidor não é bloqueada enquanto a query é executada.
    Isso permite que o servidor atenda a mais requisições.
*/

/// <summary>
/// Controller para a entidade Regiao.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RegioesController : ControllerBase
{
    private readonly NZWalksDbContext _context;
    private readonly IRegiaoRepository _regiaoRepository;
    private readonly IMapper _mapper;

    public RegioesController(NZWalksDbContext context, IRegiaoRepository regiaoRepository, IMapper mapper)
    {
        _context = context;
        _regiaoRepository = regiaoRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Action para obter todas as regiões.
    /// </summary>
    /// <returns> Lista de regiões. </returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        // Obtendo dados do banco de dados. Domain Model.
        var regioes = await _regiaoRepository.ObterTodosAsync();

        // Mapeando os dados para um DTO (Data Transfer Object).
        // Ok() é um método que retorna um status 200.
        return Ok(_mapper.Map<List<RegiaoDTO>>(regioes));
    }

    /// <summary>
    /// Action para obter uma região pelo seu ID.
    /// </summary>
    /// <param name="id"> ID da região. </param>
    /// <returns> Região. </returns>
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> ObterPorId([FromRoute] Guid id) // FromRoute é um atributo que indica que o parâmetro vem da rota.
    {
        // Obtendo dados do banco de dados. Domain Model.
        var regiao = await _regiaoRepository.ObterPorIdAsync(id);

        if (regiao == null)
        {
            // NotFound() é um método que retorna um status 404.
            return NotFound();
        }

        // Mapeando os dados para um DTO (Data Transfer Object).
        // Ok() é um método que retorna um status 200.
        return Ok(_mapper.Map<RegiaoDTO>(regiao));
    }

    /// <summary>
    /// Action para cadastrar uma região.
    /// </summary>
    /// <param name="request"> DTO com os dados da região. </param>
    /// <returns> Região cadastrada. </returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarRegiaoRequestDTO request)
    {
        // Mapeando ou Convertendo os dados do DTO para o Domain Model.
        var regiao = _mapper.Map<Regiao>(request);

        // Adicionando a entidade ao contexto.
        regiao = await _regiaoRepository.AdicionarAsync(regiao);

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regiaoDTO = _mapper.Map<RegiaoDTO>(regiao);

        // CreatedAtAction() é um método que retorna um status 201.
        // Ele também retorna um cabeçalho Location com a URL para obter a entidade criada.
        return CreatedAtAction(nameof(ObterPorId), new { id = regiao.Id }, regiao);
    }

    /// <summary>
    /// Action para atualizar uma região.
    /// </summary>
    /// <param name="id"> ID da região. </param>
    /// <param name="request"> DTO com os dados da região. </param>
    /// <returns> Região atualizada. </returns>
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid id, [FromBody] AtualizarRegiaoRequestDTO request)
    {
        // Obtendo dados do banco de dados. Domain Model.
        var regiao = await _regiaoRepository.ObterPorIdAsync(id);

        regiao = await _regiaoRepository.AtualizarAsync(id, regiao);

        if (regiao == null)
        {
            // NotFound() é um método que retorna um status 404.
            return NotFound();
        }

        // Mapeando os dados para um DTO (Data Transfer Object).
        // Ok() é um método que retorna um status 200.
        return Ok(_mapper.Map<RegiaoDTO>(regiao));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Deletar([FromRoute] Guid id)
    {
        var regiao = await _regiaoRepository.RemoverAsync(id);

        if (regiao == null)
        {
            // NotFound() é um método que retorna um status 404.
            return NotFound();
        }

        // Mapeando os dados para um DTO (Data Transfer Object).
        // Ok() é um método que retorna um status 200.
        return Ok(_mapper.Map<RegiaoDTO>(regiao));
    }
}