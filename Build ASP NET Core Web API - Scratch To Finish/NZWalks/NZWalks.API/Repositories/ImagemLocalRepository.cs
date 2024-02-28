using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class ImagemLocalRepository : IImagemRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NZWalksDbContext _dbContext;

    public ImagemLocalRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<Imagem> Upload(Imagem imagem)
    {
        var caminhoLocal = Path.Combine(_webHostEnvironment.ContentRootPath, "Imagens",
            $"{imagem.Nome}{imagem.Extensao}");

        using var stream = new FileStream(caminhoLocal, FileMode.Create);
        await imagem.Arquivo.CopyToAsync(stream);

        var caminhoUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Imagens/{imagem.Nome}";

        imagem.Caminho = caminhoUrl;

        await _dbContext.Imagens.AddAsync(imagem);
        await _dbContext.SaveChangesAsync();

        return imagem;
    }
}