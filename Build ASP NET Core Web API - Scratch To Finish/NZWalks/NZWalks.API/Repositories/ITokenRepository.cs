using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories;

public interface ITokenRepository
{
    string CriarTokenJWT(IdentityUser usuario, IList<string> papeis);
}