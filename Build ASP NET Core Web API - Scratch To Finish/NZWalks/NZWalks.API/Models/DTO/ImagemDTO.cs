using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class ImagemUploadRequestDTO
{
    [Required]
    public IFormFile Arquivo { get; set; }

    [Required]
    public string Nome { get; set; }

    public string? Descricao { get; set; }
}