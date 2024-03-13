using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO;

public class AtualizarVilaDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Nome { get; set; }

    public string Detalhes { get; set; }

    [Required]
    public double Avaliacao { get; set; }

    [Required]
    public int Quartos { get; set; }

    [Required]
    public int MetrosQuadrados { get; set; }

    [Required]
    public string ImagemUrl { get; set; }

    public string Comodidade { get; set; }
}
