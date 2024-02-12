using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProdutoAPI.Model.Context;

/// <summary>
/// Classe de contexto do banco de dados MySql
/// </summary>
public class MySqlContext : DbContext
{
    public MySqlContext() { }
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
}