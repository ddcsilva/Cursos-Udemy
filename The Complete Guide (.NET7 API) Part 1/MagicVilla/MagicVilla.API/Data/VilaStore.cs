using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Data;

public static class VilaStore
{
    public static List<VilaDTO> Vilas { get; set; } = new List<VilaDTO>
    {
        new VilaDTO { Id = 1, Nome = "Villa 1", Quartos = 3, MetrosQuadrados = 100 },
        new VilaDTO { Id = 2, Nome = "Villa 2", Quartos = 4, MetrosQuadrados = 150 },
        new VilaDTO { Id = 3, Nome = "Villa 3", Quartos = 5, MetrosQuadrados = 200 },
        new VilaDTO { Id = 4, Nome = "Villa 4", Quartos = 6, MetrosQuadrados = 250 },
        new VilaDTO { Id = 5, Nome = "Villa 5", Quartos = 7, MetrosQuadrados = 300 }
    };
}
