using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Models.ViewModels;

public class ProdutoViewModel
{
    public Produto Produto { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> ListaCategoria { get; set; }
}
