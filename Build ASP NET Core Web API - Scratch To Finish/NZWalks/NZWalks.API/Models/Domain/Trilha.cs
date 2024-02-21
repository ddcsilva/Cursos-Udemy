namespace NZWalks.API.Models.Domain;

/// <summary>
/// Entidade que representa uma trilha.
/// </summary>
public class Trilha
{
    // Guid é um tipo de dado que representa um identificador único.
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