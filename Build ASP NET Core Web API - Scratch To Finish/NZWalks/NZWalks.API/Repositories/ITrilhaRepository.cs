using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/// <summary>
/// Define um contrato para operações de repositório relacionadas a trilhas na Nova Zelândia, permitindo a manipulação e consulta de dados de trilhas.
/// </summary>
public interface ITrilhaRepository
{
    /// <summary>
    /// Recupera uma lista de todas as trilhas cadastradas no sistema.
    /// Este método é assíncrono e retorna uma lista contendo todas as trilhas disponíveis, facilitando a consulta e seleção de trilhas pelos usuários.
    /// </summary>
    /// <returns>Uma tarefa que, ao ser concluída, retorna uma lista de objetos <see cref="Trilha"/>.</returns>
    Task<List<Trilha>> ObterTodosAsync();

    /// <summary>
    /// Busca uma trilha específica pelo seu identificador único (ID).
    /// Este método é importante para obter detalhes de uma trilha específica, como parte de operações de consulta ou edição.
    /// </summary>
    /// <param name="id">O identificador único da trilha a ser buscada.</param>
    /// <returns>Uma tarefa que, quando concluída com sucesso, retorna o objeto <see cref="Trilha"/> correspondente, ou null se não encontrado.</returns>
    Task<Trilha?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Adiciona uma nova trilha ao sistema.
    /// Este método permite a criação de novas trilhas, expandindo as opções disponíveis para exploração e categorização de trilhas.
    /// </summary>
    /// <param name="trilha">O objeto <see cref="Trilha"/> contendo os dados da nova trilha a ser adicionada.</param>
    /// <returns>Uma tarefa que retorna o objeto <see cref="Trilha"/> adicionado, incluindo seu ID gerado pelo sistema.</returns>
    Task<Trilha> AdicionarAsync(Trilha trilha);

    /// <summary>
    /// Atualiza os dados de uma trilha existente.
    /// Este método é utilizado para modificar informações de uma trilha, como nome ou descrição, baseado em seu ID.
    /// </summary>
    /// <param name="id">O identificador único da trilha a ser atualizada.</param>
    /// <param name="trilha">Um objeto <see cref="Trilha"/> contendo as novas informações da trilha.</param>
    /// <returns>Uma tarefa que retorna o objeto <see cref="Trilha"/> atualizado, ou null se a trilha não for encontrada.</returns>
    public Task<Trilha?> AtualizarAsync(Guid id, Trilha trilha);

    /// <summary>
    /// Remove uma trilha do sistema.
    /// Este método é utilizado para excluir uma trilha, baseando-se em seu ID, o que pode ser necessário em caso de erros ou mudanças organizacionais.
    /// </summary>
    /// <param name="id">O identificador único da trilha a ser removida.</param>
    /// <returns>Uma tarefa que, quando concluída, retorna o objeto <see cref="Trilha"/> removido, ou null se a trilha não for encontrada.</returns>
    public Task<Trilha?> RemoverAsync(Guid id);
}