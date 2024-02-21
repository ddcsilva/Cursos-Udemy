using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

/// <summary>
/// Classe de contexto do Entity Framework.
/// </summary>
public class NZWalksDbContext : DbContext
{
    // DbContextOptions é uma classe genérica que recebe o tipo do contexto.
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options) { }

    // DbSet é uma propriedade que representa uma tabela no banco de dados.
    public DbSet<Dificuldade> Dificuldades { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
}
