namespace MagicVilla.API.Models;

public class Vila
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Detalhes { get; set; }
    public double Avaliacao { get; set; }
    public int MetrosQuadrados { get; set; }
    public int Quartos { get; set; }
    public string ImagemUrl { get; set; }
    public string Comodidade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
