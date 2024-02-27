using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/// <summary>
/// Implementa o padrão de repositório para a entidade Trilha, fornecendo métodos específicos para operações de acesso a dados.
/// Utiliza o Entity Framework para interagir com o banco de dados, abstraindo a complexidade das operações de CRUD (Create, Read, Update, Delete).
/// </summary>
public class TrilhaRepository : ITrilhaRepository
{
    /// <summary>
    /// Contexto do banco de dados utilizado para acessar as tabelas e realizar operações de banco de dados.
    /// </summary>
    private readonly NZWalksDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="TrilhaRepository"/> com o contexto do banco de dados injetado.
    /// </summary>
    /// <param name="context">O contexto do banco de dados fornecido via injeção de dependência.</param>
    public TrilhaRepository(NZWalksDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todas as trilhas cadastradas no banco de dados, com a opção de filtrar os resultados baseado em um critério específico.
    /// A filtragem é opcional e, quando aplicada, permite refinar os resultados da busca com base no nome da trilha.
    /// </summary>
    /// <param name="filtroCritério">Critério pelo qual as trilhas serão filtradas. Atualmente, suporta apenas filtragem pelo "Nome".</param>
    /// <param name="termoBusca">O termo de busca utilizado para filtrar as trilhas. Corresponde ao nome ou parte do nome da trilha.</param>
    /// <returns>Uma lista de trilhas, possivelmente filtrada com base no critério e termo de busca fornecidos.</returns>
    public async Task<List<Trilha>> ObterTodosAsync(string? filtroCritério = null, string? termoBusca = null,
        string? ordenarPor = null, bool ascendente = true, int pagina = 1, int tamanhoPagina = 10)
    {
        var trilhas = _context.Trilhas.Include("Dificuldade").Include("Regiao").AsQueryable();

        // Aplica o filtro de busca, se fornecido
        if (!string.IsNullOrWhiteSpace(filtroCritério) && !string.IsNullOrWhiteSpace(termoBusca))
        {
            if (filtroCritério.Equals("Nome", StringComparison.OrdinalIgnoreCase))
            {
                trilhas = trilhas.Where(t => t.Nome.Contains(termoBusca));
            }
        }

        // Aplica a ordenação, se fornecida
        if (!string.IsNullOrWhiteSpace(ordenarPor))
        {
            if (ordenarPor.Equals("Nome", StringComparison.OrdinalIgnoreCase))
            {
                trilhas = ascendente ? trilhas.OrderBy(t => t.Nome) : trilhas.OrderByDescending(t => t.Nome);
            }
            else if (ordenarPor.Equals("Distancia", StringComparison.OrdinalIgnoreCase))
            {
                trilhas = ascendente ? trilhas.OrderBy(t => t.DistanciaEmKm) : trilhas.OrderByDescending(t => t.DistanciaEmKm);
            }
        }

        // Aplica a paginação
        var pularRegistros = (pagina - 1) * tamanhoPagina; // Calcula quantos registros devem ser pulados

        // Retorna a lista de trilhas, aplicando a paginação
        return await trilhas.Skip(pularRegistros).Take(tamanhoPagina).ToListAsync();
    }

    /// <summary>
    /// Obtém uma trilha específica pelo seu identificador único (ID) de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da trilha a ser buscada.</param>
    /// <returns>A trilha encontrada ou null se não existir.</returns>
    public async Task<Trilha?> ObterPorIdAsync(Guid id)
    {
        return await _context.Trilhas.Include("Dificuldade").Include("Regiao").FirstOrDefaultAsync(trilha => trilha.Id == id);
    }

    /// <summary>
    /// Adiciona uma nova trilha ao banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="trilha">A trilha a ser adicionada.</param>
    /// <returns>A trilha adicionada com seu ID gerado.</returns>
    public async Task<Trilha> AdicionarAsync(Trilha trilha)
    {
        await _context.Trilhas.AddAsync(trilha);
        await _context.SaveChangesAsync();
        return trilha;
    }

    /// <summary>
    /// Atualiza os dados de uma trilha existente no banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da trilha a ser atualizada.</param>
    /// <param name="trilha">A trilha com os novos dados.</param>
    /// <returns>A trilha atualizada ou null se a trilha não existir.</returns>
    public async Task<Trilha?> AtualizarAsync(Guid id, Trilha trilha)
    {
        var trilhaExistente = await _context.Trilhas.FirstOrDefaultAsync(trilha => trilha.Id == id);
        if (trilhaExistente == null)
        {
            return null;
        }

        trilhaExistente.Nome = trilha.Nome;
        trilhaExistente.Descricao = trilha.Descricao;
        trilhaExistente.DistanciaEmKm = trilha.DistanciaEmKm;
        trilhaExistente.ImagemUrl = trilha.ImagemUrl;
        trilhaExistente.DificuldadeId = trilha.DificuldadeId;
        trilhaExistente.RegiaoId = trilha.RegiaoId;

        await _context.SaveChangesAsync();
        return trilhaExistente;
    }

    /// <summary>
    /// Remove uma trilha do banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da trilha a ser removida.</param>
    /// <returns>A trilha removida ou null se a trilha não existir.</returns>
    public async Task<Trilha?> RemoverAsync(Guid id)
    {
        var trilha = await _context.Trilhas.FirstOrDefaultAsync(trilha => trilha.Id == id);
        if (trilha == null)
        {
            return null;
        }

        _context.Trilhas.Remove(trilha);
        await _context.SaveChangesAsync();
        return trilha;
    }
}