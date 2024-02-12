using AutoMapper;
using GeekShopping.ProdutoAPI.Data.ValueObjects;
using GeekShopping.ProdutoAPI.Model;

namespace GeekShopping.ProdutoAPI.Config;

public class MappingConfig : Profile
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProdutoVO, Produto>();
            config.CreateMap<Produto, ProdutoVO>();
        });

        return mappingConfig;
    }
}
