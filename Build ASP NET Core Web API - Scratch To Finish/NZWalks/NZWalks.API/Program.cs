using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));

// Configuração da injeção de dependência.
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>(); // Lê-se: "Sempre que um objeto do tipo IRegiaoRepository for necessário, crie um objeto do tipo RegiaoSQLRepository."
builder.Services.AddScoped<ITrilhaRepository, TrilhaRepository>(); // Lê-se: "Sempre que um objeto do tipo ITrilhaRepository for necessário, crie um objeto do tipo TrilhaSQLRepository."

// Configura o AutoMapper para mapear automaticamente as entidades de domínio para os DTOs e vice-versa.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// Configuração da autenticação de usuários.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Define o esquema de autenticação padrão como JWT.
    .AddJwtBearer(options => // Adiciona o esquema de autenticação JWT.
        options.TokenValidationParameters = new TokenValidationParameters // Define os parâmetros de validação do token JWT.
        {
            ValidateIssuer = true, // Valida o emissor do token.
            ValidateAudience = true, // Valida o público-alvo do token.
            ValidateLifetime = true, // Valida o tempo de vida do token.
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do emissor.
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Define o emissor válido como o especificado no arquivo de configuração.
            ValidAudience = builder.Configuration["Jwt:Audience"], // Define o público-alvo válido como o especificado no arquivo de configuração.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Define a chave de assinatura como a especificada no arquivo de configuração.
        });

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

// Middleware que habilita a autenticação de usuários.
app.UseAuthentication();

// Middleware que habilita a autorização de usuários.
app.UseAuthorization();

// Middleware que habilita a rota de requisições para os controllers.
app.MapControllers();

// Inicializa o aplicativo.
app.Run();