using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

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