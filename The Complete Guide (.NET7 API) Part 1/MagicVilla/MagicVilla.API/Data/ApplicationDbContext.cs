using MagicVilla.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Vila> Vilas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vila>().HasData(
            new Vila
            {
                Id = 1,
                Nome = "Royal Vila",
                Detalhes = "Vila com 3 quartos, 2 banheiros, piscina e churrasqueira",
                ImagemUrl = "https://www.imovelweb.com.br/noticias/wp-content/uploads/2019/11/royal-villa-1.jpg",
                Quartos = 3,
                Avaliacao = 4.5,
                MetrosQuadrados = 200,
                Comodidade = "Piscina, Churrasqueira, 3 quartos, 2 banheiros",
                DataCriacao = DateTime.Now
            },
            new Vila
            {
                Id = 2,
                Nome = "Vila do Lago",
                Detalhes = "Vila com 2 quartos, 1 banheiro, piscina e churrasqueira",
                ImagemUrl = "https://www.imovelweb.com.br/noticias/wp-content/uploads/2019/11/vila-do-lago-1.jpg",
                Quartos = 2,
                Avaliacao = 4.0,
                MetrosQuadrados = 150,
                Comodidade = "Piscina, Churrasqueira, 2 quartos, 1 banheiro",
                DataCriacao = DateTime.Now
            },
            new Vila
            {
                Id = 3,
                Nome = "Vila do Sol",
                Detalhes = "Vila com 4 quartos, 3 banheiros, piscina e churrasqueira",
                ImagemUrl = "https://www.imovelweb.com.br/noticias/wp-content/uploads/2019/11/vila-do-sol-1.jpg",
                Quartos = 4,
                Avaliacao = 4.8,
                MetrosQuadrados = 250,
                Comodidade = "Piscina, Churrasqueira, 4 quartos, 3 banheiros",
                DataCriacao = DateTime.Now
            },
            new Vila
            {
                Id = 4,
                Nome = "Vila do Mar",
                Detalhes = "Vila com 5 quartos, 4 banheiros, piscina e churrasqueira",
                ImagemUrl = "https://www.imovelweb.com.br/noticias/wp-content/uploads/2019/11/vila-do-mar-1.jpg",
                Quartos = 5,
                Avaliacao = 4.9,
                MetrosQuadrados = 300,
                Comodidade = "Piscina, Churrasqueira, 5 quartos, 4 banheiros",
                DataCriacao = DateTime.Now
            },
            new Vila
            {
                Id = 5,
                Nome = "Vila do Céu",
                Detalhes = "Vila com 6 quartos, 5 banheiros, piscina e churrasqueira",
                ImagemUrl = "https://www.imovelweb.com.br/noticias/wp-content/uploads/2019/11/vila-do-ceu-1.jpg",
                Quartos = 6,
                Avaliacao = 5.0,
                MetrosQuadrados = 350,
                Comodidade = "Piscina, Churrasqueira, 6 quartos, 5 banheiros",
                DataCriacao = DateTime.Now
            }
        );
    }
}