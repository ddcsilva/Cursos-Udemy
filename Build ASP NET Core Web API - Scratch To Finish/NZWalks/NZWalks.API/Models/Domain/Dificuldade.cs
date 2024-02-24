namespace NZWalks.API.Models.Domain;

/// <summary>
/// Representa os diferentes níveis de dificuldade de trilhas no sistema NZWalks.
/// Esta classe é usada para categorizar as trilhas com base na sua dificuldade, facilitando aos usuários a escolha de trilhas adequadas às suas habilidades e experiências.
/// </summary>
public class Dificuldade
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
}