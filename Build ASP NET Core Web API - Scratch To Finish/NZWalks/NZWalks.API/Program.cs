using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte para controllers ao aplicativo.
builder.Services.AddControllers();

// Adiciona suporte para a documentação da API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZWalks API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configuração do banco de dados.
builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
builder.Services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));

// Configuração da injeção de dependência.
builder.Services.AddScoped<IRegiaoRepository, RegiaoRepository>(); // Lê-se: "Sempre que um objeto do tipo IRegiaoRepository for necessário, crie um objeto do tipo RegiaoSQLRepository."
builder.Services.AddScoped<ITrilhaRepository, TrilhaRepository>(); // Lê-se: "Sempre que um objeto do tipo ITrilhaRepository for necessário, crie um objeto do tipo TrilhaSQLRepository."
builder.Services.AddScoped<ITokenRepository, TokenRepository>(); // Lê-se: "Sempre que um objeto do tipo ITokenRepository for necessário, crie um objeto do tipo TokenRepository."

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

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
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