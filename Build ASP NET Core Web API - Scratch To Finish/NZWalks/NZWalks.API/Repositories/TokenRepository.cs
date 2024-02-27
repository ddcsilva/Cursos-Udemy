using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWalks.API.Repositories;

/// <summary>
/// Classe responsável por representar o repositório de tokens.
/// </summary>
public class TokenRepository : ITokenRepository
{
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Cria um token JWT para um usuário com os papéis fornecidos.
    /// </summary>
    /// <param name="usuario">O usuário para o qual o token será criado.</param>
    /// <param name="papeis">Os papéis que o usuário possui.</param>
    /// <returns>O token JWT criado.</returns>
    public string CriarTokenJWT(IdentityUser usuario, IList<string> papeis)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        foreach (var papel in papeis)
        {
            claims.Add(new Claim(ClaimTypes.Role, papel));
        }

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}