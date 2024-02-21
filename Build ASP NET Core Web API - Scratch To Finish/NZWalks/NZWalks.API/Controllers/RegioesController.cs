using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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
        /*
            Mocks para exemplo:
            Id: 1
            Codigo: NOR
            Nome: Northland
            ImagemUrl: https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/northland/northland-landscape.jpg

            Id: 2
            Codigo: AUC
            Nome: Auckland
            ImagemUrl: https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland/auckland-landscape.jpg

            Id: 3
            Codigo: WAI
            Nome: Waikato
            ImagemUrl: https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato/waikato-landscape.jpg
        */

        // Obtendo dados do banco de dados. Domain Model.
        // ToList() é um método de extensão que executa a query no banco de dados e retorna uma lista.
        var regioes = _context.Regioes.ToList();

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regioesDTO = new List<RegiaoDTO>();
        foreach (var regiaoDomain in regioes)
        {
            regioesDTO.Add(new RegiaoDTO
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

    /// <summary>
    /// Action para obter uma região pelo seu ID.
    /// </summary>
    /// <param name="id"> ID da região. </param>
    /// <returns> Região. </returns>
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
        var regiaoDTO = new RegiaoDTO
        {
            Id = regiao.Id,
            Codigo = regiao.Codigo,
            Nome = regiao.Nome,
            ImagemUrl = regiao.ImagemUrl
        };

        return Ok(regiaoDTO);
    }

    /// <summary>
    /// Action para cadastrar uma região.
    /// </summary>
    /// <param name="request"> DTO com os dados da região. </param>
    /// <returns> Região cadastrada. </returns>
    [HttpPost]
    public IActionResult Cadastrar([FromBody] CadastrarRegiaoRequestDTO request)
    {
        // Mapeando ou Convertendo os dados do DTO para o Domain Model.
        var regiao = new Regiao
        {
            Codigo = request.Codigo,
            Nome = request.Nome,
            ImagemUrl = request.ImagemUrl
        };

        // Adicionando a entidade ao contexto.
        _context.Regioes.Add(regiao);
        _context.SaveChanges();

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regiaoDTO = new RegiaoDTO
        {
            Id = regiao.Id,
            Codigo = regiao.Codigo,
            Nome = regiao.Nome,
            ImagemUrl = regiao.ImagemUrl
        };

        // CreatedAtAction() é um método que retorna um status 201.
        // Ele também retorna um cabeçalho Location com a URL para obter a entidade criada.
        return CreatedAtAction(nameof(ObterPorId), new { id = regiao.Id }, regiao);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Atualizar([FromRoute] Guid id, [FromBody] AtualizarRegiaoRequestDTO request)
    {
        // Obtendo dados do banco de dados. Domain Model.
        // FirstOrDefault() é um método do Entity Framework que busca uma entidade com base em um predicado.
        var regiao = _context.Regioes.FirstOrDefault(r => r.Id == id);

        if (regiao == null)
        {
            return NotFound();
        }

        // Mapendo os dados do DTO para o Domain Model.
        regiao.Codigo = request.Codigo;
        regiao.Nome = request.Nome;
        regiao.ImagemUrl = request.ImagemUrl;

        // Atualizando a entidade no contexto.
        // Não é necessário chamar o método Update() do DbSet, pois o Entity Framework rastreia as entidades.
        _context.SaveChanges();

        // Mapeando os dados para um DTO (Data Transfer Object).
        var regiaoDTO = new RegiaoDTO
        {
            Id = regiao.Id,
            Codigo = regiao.Codigo,
            Nome = regiao.Nome,
            ImagemUrl = regiao.ImagemUrl
        };

        // Ok() é um método que retorna um status 200.
        return Ok(regiaoDTO);
    }
}
