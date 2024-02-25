using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte para controllers ao aplicativo.
builder.Services.AddControllers();

// Adiciona suporte para a documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do banco de dados.
builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

// Configuração da injeção de dependência.
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>(); // Lê-se: "Sempre que um objeto do tipo IRegiaoRepository for necessário, crie um objeto do tipo RegiaoSQLRepository."
builder.Services.AddScoped<ITrilhaRepository, TrilhaRepository>(); // Lê-se: "Sempre que um objeto do tipo ITrilhaRepository for necessário, crie um objeto do tipo TrilhaSQLRepository."

// Configura o AutoMapper para mapear automaticamente as entidades de domínio para os DTOs e vice-versa.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

var app = builder.Build();

// Configuração do ambiente de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    // Middleware que habilita a documentação da API.
    app.UseSwagger();
    // Middleware que habilita a interface de usuário do Swagger.
    app.UseSwaggerUI();
}

// Redireciona todas as requisições HTTP para HTTPS.
app.UseHttpsRedirection();

// Middleware que habilita a autorização de usuários.
app.UseAuthorization();

// Middleware que habilita a rota de requisições para os controllers.
app.MapControllers();

// Inicializa o aplicativo.
app.Run();