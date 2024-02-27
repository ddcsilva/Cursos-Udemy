using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

/// <summary>
/// Classe responsável por representar o objeto de requisição de registro.
/// </summary>
public class RegistroRequestDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Usuario { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    public string[] Papeis { get; set; }
}