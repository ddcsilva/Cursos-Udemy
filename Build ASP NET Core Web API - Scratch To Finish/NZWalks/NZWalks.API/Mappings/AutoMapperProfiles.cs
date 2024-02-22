using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings;

/*
    AutoMapper é uma biblioteca que mapeia entidades para DTOs e vice-versa.
    Isso é útil para desacoplar a lógica de negócios da lógica de apresentação.
    No construtor da classe, é possível configurar o mapeamento de entidades para DTOs e vice-versa.
    Exemplo:
    CreateMap<Regiao, RegiaoDTO>(); -> Mapeia a entidade Regiao para a DTO RegiaoDTO.
    CreateMap<RegiaoDTO, Regiao>(); -> Mapeia a DTO RegiaoDTO para a entidade Regiao.
    Ou podemos usar o método ReverseMap() para mapear as propriedades de forma bidirecional.
    Exemplo:
    CreateMap<Regiao, RegiaoDTO>().ReverseMap(); -> Mapeia a entidade Regiao para a DTO RegiaoDTO e vice-versa.

    Quando os atributos das entidades e das DTOs têm o mesmo nome, não é necessário configurar o mapeamento.
    Exemplo:
    public class UsuarioDTO
    {
        public string NomeCompleto { get; set; }
    }
    public class Usuario
    {
        public string Nome { get; set; }
    }

    CreateMap<Usuario, UsuarioDTO>()
        .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome)); -> Mapeia a propriedade Nome da entidade Usuario para a propriedade NomeCompleto da DTO UsuarioDTO.
*/

/// <summary>
/// Classe para mapear entidades para DTOs e vice-versa.
/// </summary>
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Mapeamento de Regiao para RegiaoDTO e vice-versa.
        CreateMap<Regiao, RegiaoDTO>().ReverseMap();
        // Mapeamento de CadastrarRegiaoRequestDTO para Regiao e vice-versa.
        CreateMap<CadastrarRegiaoRequestDTO, Regiao>().ReverseMap();
        // Mapeamento de AtualizarRegiaoRequestDTO para Regiao e vice-versa.
        CreateMap<AtualizarRegiaoRequestDTO, Regiao>().ReverseMap();
    }
}
