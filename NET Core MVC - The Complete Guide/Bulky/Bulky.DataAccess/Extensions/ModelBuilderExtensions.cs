using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Ação", OrdemExibicao = 1 },
            new Categoria { Id = 2, Nome = "Ficção Científica", OrdemExibicao = 2 },
            new Categoria { Id = 3, Nome = "História", OrdemExibicao = 3 }
        );

        modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1,
                Titulo = "O Senhor dos Anéis",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-0",
                Autor = "J. R. R. Tolkien",
                ListaPreco = 29.99,
                Preco = 19.99,
                Preco50 = 14.99,
                Preco100 = 9.99,
                ImagemUrl = "",
                CategoriaId = 1
            },
            new Produto
            {
                Id = 2,
                Titulo = "O Hobbit",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-1",
                Autor = "J. R. R. Tolkien",
                ListaPreco = 19.99,
                Preco = 14.99,
                Preco50 = 9.99,
                Preco100 = 4.99,
                ImagemUrl = "",
                CategoriaId = 1
            },
            new Produto
            {
                Id = 3,
                Titulo = "O Silmarillion",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-2",
                Autor = "J. R. R. Tolkien",
                ListaPreco = 19.99,
                Preco = 14.99,
                Preco50 = 9.99,
                Preco100 = 4.99,
                ImagemUrl = "",
                CategoriaId = 1
            },
            new Produto
            {
                Id = 4,
                Titulo = "O Nome do Vento",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-3",
                Autor = "Patrick Rothfuss",
                ListaPreco = 19.99,
                Preco = 14.99,
                Preco50 = 9.99,
                Preco100 = 4.99,
                ImagemUrl = "",
                CategoriaId = 2
            },
            new Produto
            {
                Id = 5,
                Titulo = "O Temor do Sábio",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-4",
                Autor = "Patrick Rothfuss",
                ListaPreco = 19.99,
                Preco = 14.99,
                Preco50 = 9.99,
                Preco100 = 4.99,
                ImagemUrl = "",
                CategoriaId = 2
            },
            new Produto
            {
                Id = 6,
                Titulo = "O Aprendiz de Assassino",
                Descricao = "Livro de fantasia",
                ISBN = "978-3-16-148410-5",
                Autor = "Robin Hobb",
                ListaPreco = 19.99,
                Preco = 14.99,
                Preco50 = 9.99,
                Preco100 = 4.99,
                ImagemUrl = "",
                CategoriaId = 3
            }
        );
    }
}
