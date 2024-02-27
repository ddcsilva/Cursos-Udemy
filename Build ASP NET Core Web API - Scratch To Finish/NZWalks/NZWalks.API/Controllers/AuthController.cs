using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas à autenticação e autorização na aplicação NZWalks.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    /// <summary>
    /// Registra um novo usuário na aplicação.
    /// </summary>
    /// <param name="request">Os dados do usuário a ser registrado.</param>
    /// <returns>Uma ação que resulta em uma resposta HTTP com o resultado da operação.</returns>
    /// <response code="200">Usuário registrado com sucesso.</response>
    [HttpPost("Registro")]
    public async Task<IActionResult> Registro([FromBody] RegistroRequestDTO request)
    {
        var identityUser = new IdentityUser
        {
            UserName = request.Usuario,
            Email = request.Usuario
        };

        var identityResult = await _userManager.CreateAsync(identityUser, request.Senha);

        if (identityResult.Succeeded)
        {
            // Adicionar papeis ao usuário
            if (request.Papeis != null && request.Papeis.Any())
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, request.Papeis);

                if (identityResult.Succeeded)
                {
                    return Ok("Usuário registrado com sucesso.");
                }
            }
        }

        return BadRequest("Erro ao registrar usuário.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var usuario = await _userManager.FindByNameAsync(request.Usuario);

        if (usuario != null)
        {
            var resultadoSenhaVerificada = await _userManager.CheckPasswordAsync(usuario, request.Senha);

            if (resultadoSenhaVerificada)
            {
                var papeis = await _userManager.GetRolesAsync(usuario);

                if (papeis != null)
                {
                    var token = _tokenRepository.CriarTokenJWT(usuario, papeis.ToList());

                    var response = new LoginResponseDTO
                    {
                        Token = token
                    };

                    return Ok(response);
                }

                return Ok("Usuário autenticado com sucesso.");
            }
        }

        return BadRequest("Usuário ou senha inválidos.");
    }
}
