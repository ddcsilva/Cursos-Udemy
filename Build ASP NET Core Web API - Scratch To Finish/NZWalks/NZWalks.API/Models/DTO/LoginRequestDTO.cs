using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

/// <summary>
/// Classe responsável por representar o objeto de requisição de login.
/// </summary>
public class LoginRequestDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Usuario { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}