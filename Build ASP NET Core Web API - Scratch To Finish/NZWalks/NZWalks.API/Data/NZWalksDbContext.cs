using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

/// <summary>
/// Classe de contexto do Entity Framework Core para o NZWalks API.
/// Responsável pela configuração das conexões com o banco de dados e mapeamento das entidades.
/// </summary>
public class NZWalksDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="NZWalksDbContext"/> com as opções de configuração especificadas.
    /// </summary>
    /// <param name="options">As opções de configuração para o contexto do Entity Framework Core.</param>
    public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options) { }

    /// <summary>
    /// Representa a tabela Dificuldades no banco de dados.
    /// </summary>
    public DbSet<Dificuldade> Dificuldades { get; set; }

    /// <summary>
    /// Representa a tabela Regioes no banco de dados.
    /// </summary>
    public DbSet<Regiao> Regioes { get; set; }

    /// <summary>
    /// Representa a tabela Trilhas no banco de dados.
    /// </summary>
    public DbSet<Trilha> Trilhas { get; set; }

    /// <summary>
    /// Configura o modelo de entidade que o Entity Framework Core utilizará para criar o banco de dados.
    /// </summary>
    /// <param name="modelBuilder">Construtor de modelos utilizado para configurar as entidades para o banco de dados.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Popular a tabela Dificuldade com valores pré-definidos.
        var dificuldades = new List<Dificuldade>
        {
            new Dificuldade { Id = Guid.Parse("1fe2e80f-a470-4c6e-83b8-e3dfd740e55b"), Nome = "Fácil" },
            new Dificuldade { Id = Guid.Parse("b9bd1964-1f22-406d-8a97-3e99fc749749"), Nome = "Moderado" },
            new Dificuldade { Id = Guid.Parse("8531cd3a-eecc-4850-b7be-aa84bc665e1d"), Nome = "Difícil" }
        };

        modelBuilder.Entity<Dificuldade>().HasData(dificuldades);

        // Popular a tabela Regiao com valores pré-definidos.
        var regioes = new List<Regiao>
        {
            new Regiao { Id = Guid.Parse("f3e3e80f-a470-4c6e-83b8-e3dfd740e55b"), Codigo = "NLD", Nome = "Northland", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/north"},
            new Regiao { Id = Guid.Parse("5917e1f3-5a52-4287-8335-33b2398b42ce"), Codigo = "AKL", Nome = "Auckland", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/auckland"},
            new Regiao { Id = Guid.Parse("43534df4-6ee7-4a7c-a0f1-2d968d6f1955"), Codigo = "WKT", Nome = "Waikato", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/waikato"},
            new Regiao { Id = Guid.Parse("141f8f72-657c-4de0-8ae9-81be1eedbe48"), Codigo = "BOP", Nome = "Bay of Plenty", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/bay-of-plenty"},
            new Regiao { Id = Guid.Parse("26e28bca-df9a-491d-a4ae-50c64d9db738"), Codigo = "GIS", Nome = "Gisborne", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/gisborne"},
            new Regiao { Id = Guid.Parse("9562a65c-c458-457e-a88f-353f381e3b5f"), Codigo = "HKB", Nome = "Hawke's Bay", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/hawkes-bay"},
            new Regiao { Id = Guid.Parse("166b5002-852b-4abd-857c-b773fe759ec6"), Codigo = "TKI", Nome = "Taranaki", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation/places-to-visit/taranaki"},
            new Regiao { Id = Guid.Parse("a6223b18-2815-4ba6-a9dc-3fe7d0742b9e"), Codigo = "MWT", Nome = "Manawatu-Wanganui", ImagemUrl = "https://www.doc.govt.nz/globalassets/images/conservation/parks-and-recreation"}
        };

        modelBuilder.Entity<Regiao>().HasData(regioes);
    }
}