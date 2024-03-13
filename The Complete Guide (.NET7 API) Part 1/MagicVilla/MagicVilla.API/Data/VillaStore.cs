using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Data;

public static class VillaStore
{
    public static List<VillaDTO> Villas { get; set; } = new List<VillaDTO>
    {
        new VillaDTO { Id = 1, Nome = "Villa 1", Quartos = 3, Banheiros = 2 },
        new VillaDTO { Id = 2, Nome = "Villa 2", Quartos = 4, Banheiros = 3 },
        new VillaDTO { Id = 3, Nome = "Villa 3", Quartos = 5, Banheiros = 4 }
    };
}
