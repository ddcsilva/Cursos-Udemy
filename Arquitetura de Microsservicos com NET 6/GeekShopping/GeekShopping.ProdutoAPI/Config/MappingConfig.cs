using AutoMapper;
using GeekShopping.ProdutoAPI.Data.ValueObjects;
using GeekShopping.ProdutoAPI.Model;

namespace GeekShopping.ProdutoAPI.Config;

/// <summary>
/// Classe de configuração do AutoMapper
/// </summary>
public class MappingConfig : Profile
{
    /// <summary>
    /// Método de registro de mapeamento
    /// </summary>
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
