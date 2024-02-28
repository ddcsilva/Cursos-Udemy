using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

/// <summary>
/// </summary>
[Route("api/imagens")]
[ApiController]
public class ImagensController : ControllerBase
{
    private readonly IImagemRepository _imagemRepository;

    public ImagensController(IImagemRepository imagemRepository)
    {
        _imagemRepository = imagemRepository;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromForm] ImagemUploadRequestDTO request)
    {
        ValidarUploadArquivo(request);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var imagemDomain = new Imagem
        {
            Arquivo = request.Arquivo,
            Extensao = Path.GetExtension(request.Arquivo.FileName),
            TamanhoEmBytes = request.Arquivo.Length,
            Nome = request.Nome,
            Descricao = request.Descricao
        };

        await _imagemRepository.Upload(imagemDomain);

        return Ok(imagemDomain);
    }

    private void ValidarUploadArquivo(ImagemUploadRequestDTO request)
    {
        var extensoesPermitidas = new string[] { ".jpg", ".jpeg", ".png" };

        if (!extensoesPermitidas.Contains(Path.GetExtension(request.Arquivo.FileName)))
        {
            ModelState.AddModelError("Arquivo", "Extensão do arquivo inválida.");
        }

        if (request.Arquivo.Length > 10485760)
        {
            ModelState.AddModelError("Arquivo", "O tamanho do arquivo não pode ser maior que 10MB.");
        }
    }
}