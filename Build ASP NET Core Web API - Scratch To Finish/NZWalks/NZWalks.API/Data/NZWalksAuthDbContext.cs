using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data;

/// <summary>
/// Representa o contexto de dados para autenticação e autorização na aplicação NZWalks.
/// </summary>
public class NZWalksAuthDbContext : IdentityDbContext
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="NZWalksAuthDbContext"/>.
    /// </summary>
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options) { }

    /// <summary>
    /// Configura o modelo de dados para autenticação e autorização na aplicação.
    /// </summary>
    /// <param name="builder">O construtor de modelos de dados para o contexto.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "80eba5e1-33ee-4444-81b2-4edee2f25f99";
        var writerRoleId = "d789895d-07c0-468c-9d8c-07e110937e30";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "READER"
            },
            new IdentityRole
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "WRITER"
            }
        };

        // Adiciona os papéis de leitor e escritor ao contexto
        builder.Entity<IdentityRole>().HasData(roles);
    }
}