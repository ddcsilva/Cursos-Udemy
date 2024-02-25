using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings;

/// <summary>
/// Configura o mapeamento entre entidades de domínio e Data Transfer Objects (DTOs) usando AutoMapper.
/// Esta configuração permite a conversão automática entre os objetos de domínio (entidades) e os DTOs,
/// simplificando as operações de CRUD ao isolar a lógica de apresentação da lógica de negócios.
/// </summary>
public class AutoMapperProfiles : Profile
{
    /// <summary>
    /// Define os mapeamentos entre as entidades de domínio e os DTOs.
    /// </summary>
    public AutoMapperProfiles()
    {
        // Mapeia a entidade Regiao para RegiaoDTO e vice-versa, permitindo uma conversão bidirecional
        // entre a entidade e o DTO. Isso é útil para operações de leitura e escrita envolvendo a entidade Regiao.
        CreateMap<Regiao, RegiaoDTO>().ReverseMap();
        // Mapeia CadastrarRegiaoRequestDTO para Regiao e vice-versa. Este mapeamento é especialmente útil
        // para a criação de novas regiões, onde os dados recebidos como DTO são convertidos para uma entidade de domínio.
        CreateMap<AdicionarRegiaoRequestDTO, Regiao>().ReverseMap();
        // Mapeia AtualizarRegiaoRequestDTO para Regiao e vice-versa. Este mapeamento facilita a atualização
        // de regiões existentes, permitindo que os dados do DTO sejam mapeados diretamente para a entidade de domínio correspondente.
        CreateMap<AtualizarRegiaoRequestDTO, Regiao>().ReverseMap();
        // Mapeia a entidade Regiao para TrilhaDTO e vice-versa, permitindo uma conversão bidirecional
        // entre a entidade e o DTO. Isso é útil para operações de leitura e escrita envolvendo a entidade Trilha.
        CreateMap<Trilha, TrilhaDTO>().ReverseMap();
        // Mapeia a entidade Trilha para TrilhaDTO e vice-versa, permitindo uma conversão bidirecional
        // entre a entidade e o DTO. Isso é útil para operações de leitura e escrita envolvendo a entidade Trilha.
        CreateMap<AdicionarTrilhaRequestDTO, Trilha>().ReverseMap();
        // Mapeia AtualizarTrilhaRequestDTO para Trilha e vice-versa. Este mapeamento facilita a atualização
        // de trilhas existentes, permitindo que os dados do DTO sejam mapeados diretamente para a entidade de domínio correspondente.
        CreateMap<AtualizarTrilhaRequestDTO, Trilha>().ReverseMap();
        // Mapeia a entidade Dificuldade para DificuldadeDTO e vice-versa, permitindo uma conversão bidirecional
        // entre a entidade e o DTO. Isso é útil para operações de leitura e escrita envolvendo a entidade Dificuldade.
        CreateMap<DificuldadeDTO, Dificuldade>().ReverseMap();
    }
}