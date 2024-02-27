using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories;

/// <summary>
/// Interface responsável por representar o repositório de tokens.
/// </summary>
public interface ITokenRepository
{
    /// <summary>
    /// Cria um token JWT para um usuário com os papéis fornecidos.
    /// </summary>
    /// <param name="usuario">O usuário para o qual o token será criado.</param>
    /// <param name="papeis">Os papéis que o usuário possui.</param>
    string CriarTokenJWT(IdentityUser usuario, IList<string> papeis);
}