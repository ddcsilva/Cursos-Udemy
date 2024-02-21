using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options) { }

    public DbSet<Dificuldade> Dificuldades { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Trilha> Trilhas { get; set; }
}
