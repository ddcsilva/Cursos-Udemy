using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.API.CustomActionFilters;

/// <summary>
/// Atributo de filtro de ação que valida o estado do modelo para ações de controlador.
/// Este filtro é utilizado para verificar se o modelo passado a uma ação de controlador é válido,
/// interrompendo a execução da ação e retornando um BadRequest (400) caso o modelo seja inválido.
/// Pode ser aplicado globalmente, por controlador ou por ação específica.
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Chamado antes da execução da ação do controlador.
    /// Verifica se o estado do modelo é válido e, caso não seja, define o resultado da ação como BadRequest.
    /// </summary>
    /// <param name="context">Contexto da execução da ação, fornecendo acesso ao estado do modelo e outros dados da requisição.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Verifica a validade do modelo
        if (context.ModelState.IsValid == false)
        {
            // Define o resultado da ação como BadRequest se o modelo for inválido
            context.Result = new BadRequestResult();
        }
    }
}