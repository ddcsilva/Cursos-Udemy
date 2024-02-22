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

    /// <summary>
    /// Método para obter uma região pelo seu ID.
    /// </summary>
    /// <param name="id"> ID da região. </param>
    /// <returns> Região. </returns>
    Task<Regiao?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Método para adicionar uma região.
    /// </summary>
    /// <param name="regiao"> Região. </param>
    /// <returns> Região. </returns>
    Task<Regiao> AdicionarAsync(Regiao regiao);

    /// <summary>
    /// Método para atualizar uma região.
    /// </summary>
    /// <param name="regiao"> Região. </param>
    /// <returns> Região. </returns>
    Task<Regiao?> AtualizarAsync(Guid id, Regiao regiao);

    /// <summary>
    /// Método para remover uma região.
    /// </summary>
    /// <param name="id"> ID da região. </param>
    /// <returns> Região. </returns>
    Task<Regiao?> RemoverAsync(Guid id);
}