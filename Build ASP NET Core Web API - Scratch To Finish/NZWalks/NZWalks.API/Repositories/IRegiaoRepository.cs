using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

/// <summary>
/// Define um contrato para operações de repositório relacionadas a regiões na Nova Zelândia, permitindo a manipulação e consulta de dados de regiões.
/// </summary>
public interface IRegiaoRepository
{
    /// <summary>
    /// Recupera uma lista de todas as regiões cadastradas no sistema.
    /// Este método é assíncrono e retorna uma lista contendo todas as regiões disponíveis, facilitando a consulta e seleção de regiões pelos usuários.
    /// </summary>
    /// <returns>Uma tarefa que, ao ser concluída, retorna uma lista de objetos <see cref="Regiao"/>.</returns>
    Task<List<Regiao>> ObterTodosAsync();

    /// <summary>
    /// Busca uma região específica pelo seu identificador único (ID).
    /// Este método é importante para obter detalhes de uma região específica, como parte de operações de consulta ou edição.
    /// </summary>
    /// <param name="id">O identificador único da região a ser buscada.</param>
    /// <returns>Uma tarefa que, quando concluída com sucesso, retorna o objeto <see cref="Regiao"/> correspondente, ou null se não encontrado.</returns>
    Task<Regiao?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Adiciona uma nova região ao sistema.
    /// Este método permite a criação de novas regiões, expandindo as opções disponíveis para categorização de trilhas.
    /// </summary>
    /// <param name="regiao">O objeto <see cref="Regiao"/> contendo os dados da nova região a ser adicionada.</param>
    /// <returns>Uma tarefa que retorna o objeto <see cref="Regiao"/> adicionado, incluindo seu ID gerado pelo sistema.</returns>
    Task<Regiao> AdicionarAsync(Regiao regiao);

    /// <summary>
    /// Atualiza os dados de uma região existente.
    /// Este método é utilizado para modificar informações de uma região, como nome ou imagem, baseado em seu ID.
    /// </summary>
    /// <param name="id">O identificador único da região a ser atualizada.</param>
    /// <param name="regiao">Um objeto <see cref="Regiao"/> contendo as novas informações da região.</param>
    /// <returns>Uma tarefa que retorna o objeto <see cref="Regiao"/> atualizado, ou null se a região não for encontrada.</returns>
    Task<Regiao?> AtualizarAsync(Guid id, Regiao regiao);

    /// <summary>
    /// Remove uma região do sistema.
    /// Este método é utilizado para excluir uma região, baseando-se em seu ID, o que pode ser necessário em caso de erros ou mudanças organizacionais.
    /// </summary>
    /// <param name="id">O identificador único da região a ser removida.</param>
    /// <returns>Uma tarefa que, quando concluída, retorna o objeto <see cref="Regiao"/> removido, ou null se a região não for encontrada.</returns>
    Task<Regiao?> RemoverAsync(Guid id);
}