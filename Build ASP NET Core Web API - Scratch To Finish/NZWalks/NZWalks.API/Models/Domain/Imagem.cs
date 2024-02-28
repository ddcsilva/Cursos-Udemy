using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain;

public class Imagem
{
    public Guid Id { get; set; }

    [NotMapped]
    public IFormFile Arquivo { get; set; }

    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public string Extensao { get; set; }
    public long TamanhoEmBytes { get; set; }
    public string Caminho { get; set; }
}