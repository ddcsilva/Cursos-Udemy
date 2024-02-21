using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers;

/// <summary>
/// Controller para a entidade Regiao.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RegioesController : ControllerBase
{
    private readonly NZWalksDbContext _context;

    public RegioesController(NZWalksDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Action para obter todas as regiões.
    /// </summary>
    /// <returns> Lista de regiões. </returns>
    [HttpGet]
    public IActionResult ObterTodos()
    {
        // Obtendo dados do banco de dados. Domain Model.
        // ToList() é um método de extensão que executa a query no banco de dados e retorna uma lista.
        var regioes = _context.Regioes.ToList();

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regioesDto = new List<RegiaoDTO>();
        foreach (var regiaoDomain in regioes)
        {
            regioesDto.Add(new RegiaoDTO
            {
                Id = regiaoDomain.Id,
                Codigo = regiaoDomain.Codigo,
                Nome = regiaoDomain.Nome,
                ImagemUrl = regiaoDomain.ImagemUrl
            });
        }

        // Ok() é um método que retorna um status 200.
        return Ok(regioes);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult ObterPorId([FromRoute] Guid id) // FromRoute é um atributo que indica que o parâmetro vem da rota.
    {
        // Obtendo dados do banco de dados. Domain Model.
        // Find() é um método do Entity Framework que busca uma entidade pelo seu ID.
        var regiao = _context.Regioes.Find(id);

        if (regiao == null)
        {
            // NotFound() é um método que retorna um status 404.
            return NotFound();
        }

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regiaoDto = new RegiaoDTO
        {
            Id = regiao.Id,
            Codigo = regiao.Codigo,
            Nome = regiao.Nome,
            ImagemUrl = regiao.ImagemUrl
        };

        return Ok(regiaoDto);
    }
}
