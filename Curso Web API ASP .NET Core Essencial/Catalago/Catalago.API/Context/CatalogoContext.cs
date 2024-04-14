using Catalago.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalago.Api.Context;

/// <summary>
/// Classe que representa o contexto do banco de dados
/// </summary>
/// <param name="options"> Opções de configuração do contexto </param>
public class CatalogoContext(DbContextOptions<CatalogoContext> options) : DbContext(options)
{
    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Categoria>? Categorias { get; set; }
}