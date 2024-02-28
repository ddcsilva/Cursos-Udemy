using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface IImagemRepository
{
    Task<Imagem> Upload(Imagem imagem);
}