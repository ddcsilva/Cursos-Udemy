﻿namespace NZWalks.API.Models.Domain;

public class Trilha
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double DistanciaEmKm { get; set; }
    public string? ImagemUrl { get; set; }
    public Guid DificuldadeId { get; set; }
    public Guid RegiaoId { get; set; }


    // Propriedades de Navegação
    public Dificuldade Dificuldade { get; set; }
    public Regiao Regiao { get; set; }
}