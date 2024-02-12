using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProdutoAPI.Model.Base;

/// <summary>
/// Classe base para as entidades
/// </summary>
public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}
