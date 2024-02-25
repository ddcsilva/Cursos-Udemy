using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/// <summary>
/// Implementa o padrão de repositório para a entidade Região, fornecendo métodos específicos para operações de acesso a dados.
/// Utiliza o Entity Framework para interagir com o banco de dados, abstraindo a complexidade das operações de CRUD (Create, Read, Update, Delete).
/// </summary>
public class RegiaoRepository : IRegiaoRepository
{
    /// <summary>
    /// Contexto do banco de dados utilizado para acessar as tabelas e realizar operações de banco de dados.
    /// </summary>
    private readonly NZWalksDbContext _context;

    /// <summary>
    /// Inicializa uma nova instância do <see cref="RegiaoRepository"/> com o contexto do banco de dados injetado.
    /// </summary>
    /// <param name="context">O contexto do banco de dados fornecido via injeção de dependência.</param>
    public RegiaoRepository(NZWalksDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todas as regiões cadastradas no banco de dados de forma assíncrona.
    /// </summary>
    /// <returns>Uma lista de regiões.</returns>
    public async Task<List<Regiao>> ObterTodosAsync()
    {
        return await _context.Regioes.ToListAsync();
    }

    /// <summary>
    /// Busca uma região pelo seu identificador único (ID) de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da região a ser encontrada.</param>
    /// <returns>A região encontrada ou null se não existir.</returns>
    public async Task<Regiao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Regioes.FirstOrDefaultAsync(regiao => regiao.Id == id);
    }

    /// <summary>
    /// Adiciona uma nova região ao banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="regiao">A região a ser adicionada.</param>
    /// <returns>A região adicionada com seu ID gerado.</returns>
    public async Task<Regiao> AdicionarAsync(Regiao regiao)
    {
        await _context.Regioes.AddAsync(regiao);
        await _context.SaveChangesAsync();
        return regiao;
    }

    /// <summary>
    /// Atualiza os dados de uma região existente no banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da região a ser atualizada.</param>
    /// <param name="regiao">Os novos dados da região.</param>
    /// <returns>A região atualizada ou null se a região não existir.</returns>
    public async Task<Regiao?> AtualizarAsync(Guid id, Regiao regiao)
    {
        var regiaoEncontrada = await _context.Regioes.FirstOrDefaultAsync(r => r.Id == id);
        if (regiaoEncontrada == null)
        {
            return null;
        }

        regiaoEncontrada.Codigo = regiao.Codigo;
        regiaoEncontrada.Nome = regiao.Nome;
        regiaoEncontrada.ImagemUrl = regiao.ImagemUrl;
        await _context.SaveChangesAsync();
        return regiaoEncontrada;
    }

    /// <summary>
    /// Remove uma região do banco de dados de forma assíncrona.
    /// </summary>
    /// <param name="id">O ID da região a ser removida.</param>
    /// <returns>A região removida ou null se a região não existir.</returns>
    public async Task<Regiao?> RemoverAsync(Guid id)
    {
        var regiao = await _context.Regioes.FirstOrDefaultAsync(r => r.Id == id);
        if (regiao == null)
        {
            return null;
        }

        _context.Regioes.Remove(regiao);
        await _context.SaveChangesAsync();
        return regiao;
    }
}