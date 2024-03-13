using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Config;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Vila, VilaDTO>();
        CreateMap<VilaDTO, Vila>();

        CreateMap<Vila, CriarVilaDTO>().ReverseMap();
        CreateMap<Vila, AtualizarVilaDTO>().ReverseMap();
    }
}