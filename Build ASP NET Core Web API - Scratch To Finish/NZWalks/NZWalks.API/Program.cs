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

// Adiciona suporte para a geração de tokens JWT.
builder.Services.AddSwaggerGen(options =>
{
    // Define as informações da documentação da API.
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZWalks API", Version = "v1" });
    // Define o esquema de autenticação JWT como o esquema padrão.
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization", // Define o nome do cabeçalho de autorização.
        In = ParameterLocation.Header, // Define a localização do cabeçalho de autorização.
        Type = SecuritySchemeType.ApiKey, // Define o tipo do esquema de segurança como chave de API.
        Scheme = JwtBearerDefaults.AuthenticationScheme // Define o esquema de segurança como o esquema de autenticação JWT.
    });

    // Define os requisitos de segurança da documentação da API.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            // Define o esquema de segurança JWT como requisito de segurança.
            new OpenApiSecurityScheme
            {
                // Define o tipo do esquema de segurança como referência de segurança.
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // Define o tipo da referência de segurança como esquema de segurança.
                    Id = JwtBearerDefaults.AuthenticationScheme // Define o identificador da referência de segurança como o esquema de autenticação JWT.
                },
                Scheme = "oauth2", // Define o esquema de segurança como OAuth 2.0.
                Name = JwtBearerDefaults.AuthenticationScheme, // Define o nome do esquema de segurança como o esquema de autenticação JWT.
                In = ParameterLocation.Header // Define a localização do esquema de segurança como cabeçalho.
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

// Configuração da autorização de usuários.
builder.Services.AddIdentityCore<IdentityUser>() // Adiciona o esquema de identidade de usuários.
    .AddRoles<IdentityRole>() // Adiciona o esquema de papéis de usuários.
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks") // Adiciona o provedor de tokens de proteção de dados para o esquema de identidade de usuários.
    .AddEntityFrameworkStores<NZWalksAuthDbContext>() // Adiciona o esquema de armazenamento de entidades de domínio para o esquema de identidade de usuários.
    .AddDefaultTokenProviders(); // Adiciona os provedores de tokens padrão para o esquema de identidade de usuários.

// Configuração das opções de identidade de usuários.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; // Define que a senha não precisa conter dígitos.
    options.Password.RequireLowercase = false; // Define que a senha não precisa conter letras minúsculas.
    options.Password.RequireNonAlphanumeric = false; // Define que a senha não precisa conter caracteres não alfanuméricos.
    options.Password.RequireUppercase = false; // Define que a senha não precisa conter letras maiúsculas.
    options.Password.RequiredLength = 6; // Define que a senha precisa ter no mínimo 6 caracteres.
    options.Password.RequiredUniqueChars = 1; // Define que a senha precisa ter no mínimo 1 caractere único.
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