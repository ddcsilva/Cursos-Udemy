using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Web.Areas.Admin.Controllers;

/// <summary>
/// Controller responsável por gerenciar os produtos.
/// </summary>
[Area("Admin")]
public class ProdutoController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProdutoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Action responsável por listar os produtos.
    /// </summary>
    /// <returns> View com a lista de produtos. </returns>
    [HttpGet]
    public IActionResult Index()
    {
        // Busca todos os produtos do banco de dados.
        var produtos = _unitOfWork.Produto.ObterTodos().ToList();

        // Retorna a view com a lista de produtos.
        return View(produtos);
    }

    /// <summary>
    /// Action responsável por adicionar ou atualizar um produto.
    /// </summary>
    /// <returns> View com o formulário de adição ou atualização de um produto. </returns>
    [HttpGet]
    public IActionResult AdicionarAtualizar(int? id)
    {
        var produtoViewModel = new ProdutoViewModel
        {
            Produto = new Produto(),

            // Busca todas as categorias do banco de dados e converte para um SelectListItem
            ListaCategoria = _unitOfWork.Categoria.ObterTodos().Select(c => new SelectListItem
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            })
        };

        // Verifica se o id é nulo ou menor ou igual a zero
        if (id == null || id <= 0)
        {
            // Retorna a view com o formulário de adição de produto
            return View(produtoViewModel);
        }
        else
        {
            // Busca o produto no banco de dados
            produtoViewModel.Produto = _unitOfWork.Produto.Obter(p => p.Id == id);

            // Verifica se o produto foi encontrado
            if (produtoViewModel.Produto == null)
            {
                // Retorna um erro HTTP 404
                return NotFound();
            }

            return View(produtoViewModel);
        }
    }

    /// <summary>
    /// Action responsável por adicionar ou atualizar um produto.
    /// </summary>
    /// <param name="produto"> Objeto produto preenchido com os dados do formulário. </param>
    /// <returns> Redireciona para a action Index ou retorna a view com o formulário preenchido. </returns>
    [HttpPost]
    public IActionResult AdicionarAtualizar(ProdutoViewModel produtoViewModel, IFormFile? imagem)
    {
        // Verifica se o modelo é válido
        if (ModelState.IsValid)
        {
            // Obtém o caminho da pasta wwwroot
            var wwwRootPath = _webHostEnvironment.WebRootPath;

            // Verifica se o arquivo foi enviado
            if (imagem != null)
            {
                // Obtém a extensão do arquivo
                var extensao = Path.GetExtension(imagem.FileName);
                // Gera um nome único para o arquivo
                var nomeArquivo = Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(produtoViewModel.Produto.ImagemUrl))
                {
                    // Obtém o caminho completo do arquivo existente
                    // TrimStart: remove a barra invertida do início da string
                    var caminhoArquivoExistente = Path.Combine(wwwRootPath, produtoViewModel.Produto.ImagemUrl.TrimStart('\\'));

                    // Verifica se o arquivo existe
                    if (System.IO.File.Exists(caminhoArquivoExistente))
                    {
                        // Remove o arquivo existente
                        System.IO.File.Delete(caminhoArquivoExistente);
                    }
                }

                // Obtém o caminho completo do diretório onde será salvo o arquivo  
                var caminhoArquivo = Path.Combine(wwwRootPath, @"imagens\produtos");

                // Cria o diretório se ele não existir
                // FileMode.Create: cria um novo arquivo. Se o arquivo já existir, ele será substituído.
                using (var fileStream = new FileStream(Path.Combine(caminhoArquivo, nomeArquivo + extensao), FileMode.Create))
                {
                    // Salva o arquivo no diretório
                    imagem.CopyTo(fileStream);
                }

                // Monta o caminho do arquivo para salvar no banco de dados
                produtoViewModel.Produto.ImagemUrl = @"\imagens\produtos\" + nomeArquivo + extensao;
            }

            // Verifica se o produto já existe no banco de dados
            if (produtoViewModel.Produto.Id > 0)
            {
                // Atualiza o produto no contexto do banco de dados
                _unitOfWork.Produto.Atualizar(produtoViewModel.Produto);
            }
            else
            {
                // Adiciona o produto ao contexto do banco de dados
                _unitOfWork.Produto.Adicionar(produtoViewModel.Produto);
            }

            // Salva as alterações no banco de dados
            _unitOfWork.Salvar();
            // Adiciona uma mensagem de sucesso na sessão
            TempData["MensagemSucesso"] = "Produto adicionado com sucesso.";

            // Redireciona para a action Index
            return RedirectToAction(nameof(Index));
        }
        else
        {
            produtoViewModel.ListaCategoria = _unitOfWork.Categoria.ObterTodos().Select(c => new SelectListItem
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            });

            // Se o modelo não for válido, retorna a view com o formulário preenchido
            return View(produtoViewModel);
        }
    }

    [HttpGet]
    public IActionResult Excluir(int? id)
    {
        // Verifica se o id é nulo ou menor ou igual a zero
        if (id == null || id <= 0)
        {
            // Retorna um erro HTTP 404
            return NotFound();
        }

        // Busca o produto no banco de dados
        var produtoParaExcluir = _unitOfWork.Produto.Obter(p => p.Id == id);

        // Verifica se o produto foi encontrado
        if (produtoParaExcluir == null)
        {
            // Retorna um erro HTTP 404
            return NotFound();
        }

        // Retorna a view com o produto a ser excluído
        return View(produtoParaExcluir);
    }

    /// <summary>
    /// Action responsável por excluir um produto.
    /// </summary>
    /// <param name="id"> Id do produto a ser excluído. </param>
    /// <returns> Redireciona para a action Index ou retorna a view com o formulário preenchido. </returns>
    [HttpPost, ActionName("Excluir")]
    public IActionResult ConfirmarExclusao(int? id)
    {
        // Busca o produto no banco de dados
        var produtoParaExcluir = _unitOfWork.Produto.Obter(p => p.Id == id);

        // Verifica se o produto foi encontrado
        if (produtoParaExcluir == null)
        {
            // Retorna um erro HTTP 404
            return NotFound();
        }

        // Remove o produto do contexto do banco de dados
        _unitOfWork.Produto.Remover(produtoParaExcluir);
        // Salva as alterações no banco de dados
        _unitOfWork.Salvar();
        // Adiciona uma mensagem de sucesso na sessão
        TempData["MensagemSucesso"] = "Produto excluído com sucesso.";

        // Redireciona para a action Index
        return RedirectToAction(nameof(Index));
    }
}
