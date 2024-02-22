using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/*
    O padrão de repositório é uma abstração que permite desacoplar a lógica de negócios da lógica de acesso a dados.
    Isso permite que a lógica de negócios seja independente da lógica de acesso a dados, facilitando a manutenção e a evolução do sistema.
    É uma boa prática de programação e é amplamente utilizada em sistemas de grande porte.
*/

/// <summary>
/// Repositório para a entidade Região.
/// </summary>
public class RegiaoSQLRepository : IRegiaoRepository
{
    // O repositório precisa de uma instância do DbContext para acessar o banco de dados.
    private readonly NZWalksDbContext _context;

    // O DbContext é injetado no construtor.
    public RegiaoSQLRepository(NZWalksDbContext context)
    {
        _context = context;
    }

    public async Task<List<Regiao>> ObterTodosAsync()
    {
        // O método ToListAsync() executa a query no banco de dados e retorna uma lista.
        return await _context.Regioes.ToListAsync();
    }

    public async Task<Regiao?> ObterPorIdAsync(Guid id)
    {
        // FindAsync() é um método do Entity Framework que busca uma entidade pelo seu ID.
        // FirstOrDefaultAsync() é um método do Entity Framework que busca a primeira entidade que satisfaça a condição.
        return await _context.Regioes.FirstOrDefaultAsync(regiao => regiao.Id == id);
    }

    public async Task<Regiao> AdicionarAsync(Regiao regiao)
    {
        // AddAsync() é um método do Entity Framework que adiciona uma entidade ao contexto.
        // SaveChangesAsync() é um método do Entity Framework que salva as mudanças no banco de dados.
        await _context.Regioes.AddAsync(regiao);
        await _context.SaveChangesAsync();
        return regiao;
    }

    public async Task<Regiao?> AtualizarAsync(Guid id, Regiao regiao)
    {
        // FirstOrDefaultAsync() é um método do Entity Framework que busca a primeira entidade que satisfaça a condição.
        var regiaoEncontrada = await _context.Regioes.FirstOrDefaultAsync(regiao => regiao.Id == id);

        if (regiaoEncontrada == null)
        {
            return null;
        }
        // Atualiza as propriedades da região encontrada com as propriedades da região passada como parâmetro.
        regiaoEncontrada.Codigo = regiao.Codigo;
        regiaoEncontrada.Nome = regiao.Nome;
        regiaoEncontrada.ImagemUrl = regiao.ImagemUrl;

        // SaveChangesAsync() é um método do Entity Framework que salva as mudanças no banco de dados.
        await _context.SaveChangesAsync();
        return regiaoEncontrada;
    }

    public async Task<Regiao?> RemoverAsync(Guid id)
    {
        // FirstOrDefaultAsync() é um método do Entity Framework que busca a primeira entidade que satisfaça a condição.
        var regiao = await _context.Regioes.FirstOrDefaultAsync(regiao => regiao.Id == id);

        if (regiao == null)
        {
            return null;
        }

        // Remove() é um método do Entity Framework que marca a entidade como removida.
        // SaveChangesAsync() é um método do Entity Framework que salva as mudanças no banco de dados.
        _context.Regioes.Remove(regiao);
        await _context.SaveChangesAsync();
        return regiao;
    }
}