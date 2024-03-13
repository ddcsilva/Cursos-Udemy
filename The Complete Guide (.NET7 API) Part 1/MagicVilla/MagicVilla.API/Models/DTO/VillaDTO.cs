using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO;

public class VillaDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Nome { get; set; }

    public int Quartos { get; set; }

    public int Banheiros { get; set; }
}
