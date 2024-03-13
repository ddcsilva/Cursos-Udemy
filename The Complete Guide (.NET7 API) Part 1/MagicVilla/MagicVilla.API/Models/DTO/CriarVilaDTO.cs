using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO;

public class CriarVilaDTO
{
    [Required]
    [MaxLength(30)]
    public string Nome { get; set; }

    public string Detalhes { get; set; }

    public double Avaliacao { get; set; }

    public int Quartos { get; set; }

    public int MetrosQuadrados { get; set; }

    public string ImagemUrl { get; set; }

    public string Comodidade { get; set; }
}
