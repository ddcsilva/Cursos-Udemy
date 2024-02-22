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
}