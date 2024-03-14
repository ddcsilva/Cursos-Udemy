using System.Net;
using AutoMapper;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using MagicVilla.API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VilasController : ControllerBase
{
    private readonly IVilaRepository _vilaRepository;
    private readonly IMapper _mapper;
    protected APIResponse _response;

    public VilasController(IVilaRepository vilaRepository, IMapper mapper)
    {
        _vilaRepository = vilaRepository;
        _mapper = mapper;
        _response = new APIResponse();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> ObterVilas()
    {
        try
        {
            IEnumerable<Vila> vilas = await _vilaRepository.ObterTodos();

            _response.StatusCode = HttpStatusCode.OK;
            _response.Resultado = _mapper.Map<IEnumerable<VilaDTO>>(vilas);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> ObterVila(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                return BadRequest(_response);
            }

            var vila = await _vilaRepository.Obter(v => v.Id == id);

            if (vila == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Sucesso = false;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Resultado = _mapper.Map<VilaDTO>(vila);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> AdicionarVila([FromBody] CriarVilaDTO criarVilaDTO)
    {
        try
        {
            if (criarVilaDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                return BadRequest(_response);
            }

            if (await _vilaRepository.Obter(v => v.Nome == criarVilaDTO.Nome) != null)
            {
                ModelState.AddModelError("Nome", "Nome já existe");
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                _response.MensagensDeErro = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(_response);
            }

            Vila model = _mapper.Map<Vila>(criarVilaDTO);

            await _vilaRepository.Adicionar(model);

            _response.StatusCode = HttpStatusCode.Created;
            _response.Resultado = _mapper.Map<VilaDTO>(model);

            return CreatedAtAction(nameof(ObterVila), new { id = model.Id }, _response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> AtualizarVila(int id, [FromBody] AtualizarVilaDTO atualizarVilaDTO)
    {
        try
        {
            if (atualizarVilaDTO == null || id != atualizarVilaDTO.Id)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                return BadRequest(_response);
            }

            Vila model = _mapper.Map<Vila>(atualizarVilaDTO);

            await _vilaRepository.Atualizar(model);

            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> ExcluirVila(int id)
    {
        try
        {
            if (id <= 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                return BadRequest(_response);
            }

            var vila = await _vilaRepository.Obter(v => v.Id == id);
            if (vila == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Sucesso = false;
                return NotFound(_response);
            }

            await _vilaRepository.Remover(vila);

            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }

    [HttpPatch("{id:int}", Name = "AtualizarVilaParcial")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> AtualizarParcialVilla(int id, [FromBody] JsonPatchDocument<AtualizarVilaDTO> patch)
    {
        try
        {
            if (patch == null || id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                return BadRequest(_response);
            }

            var vila = await _vilaRepository.Obter(v => v.Id == id, false);

            AtualizarVilaDTO vilaDTO = _mapper.Map<AtualizarVilaDTO>(vila);

            if (vila == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Sucesso = false;
                return NotFound(_response);
            }

            patch.ApplyTo(vilaDTO, ModelState);

            Vila model = _mapper.Map<Vila>(vilaDTO);

            await _vilaRepository.Atualizar(model);

            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Sucesso = false;
                _response.MensagensDeErro = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Sucesso = false;
            _response.MensagensDeErro = new List<string> { ex.ToString() };
        }

        return _response;
    }
}
