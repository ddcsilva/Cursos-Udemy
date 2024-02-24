namespace NZWalks.API.Models.Domain;

/// <summary>
/// Representa uma trilha na Nova Zelândia, armazenando informações detalhadas sobre cada trilha.
/// Esta classe serve como um repositório para dados de trilhas, incluindo aspectos como nome, descrição, distância, imagem, dificuldade e a região onde a trilha está localizada.
/// Os dados coletados aqui facilitam aos usuários a busca e seleção de trilhas baseadas em diferentes critérios, como dificuldade e localização.
/// </summary>
public class Trilha
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double DistanciaEmKm { get; set; }
    public string? ImagemUrl { get; set; }
    public Guid DificuldadeId { get; set; }
    public Guid RegiaoId { get; set; }

    // Propriedades de Navegação -> São propriedades que representam relacionamentos entre entidades.
    public Dificuldade Dificuldade { get; set; }
    public Regiao Regiao { get; set; }
}