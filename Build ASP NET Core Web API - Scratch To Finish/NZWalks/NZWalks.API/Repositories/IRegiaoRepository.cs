using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/// <summary>
/// Interface para o repositório de Região.
/// </summary>
public interface IRegiaoRepository
{
    /// <summary>
    /// Método para obter todas as regiões.
    /// </summary>
    /// <returns> Lista de regiões. </returns>
    Task<List<Regiao>> ObterTodosAsync();
}