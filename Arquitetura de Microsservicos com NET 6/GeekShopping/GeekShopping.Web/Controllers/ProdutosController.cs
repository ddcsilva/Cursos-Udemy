using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.ObterTodosProdutos();
        return View(produtos);
    }

    [HttpGet]
    public async Task<IActionResult> Criar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Criar(ProdutoModel produto)
    {
        if (ModelState.IsValid)
        {
            var resultado = await _produtoService.CriarProduto(produto);

            if (resultado != null)
            {
                return RedirectToAction("Index");
            }
        }

        return View(produto);
    }

    [HttpGet]
    public async Task<IActionResult> Atualizar(int id)
    {
        var produto = await _produtoService.ObterProdutoPorId(id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Atualizar(ProdutoModel produto)
    {
        if (ModelState.IsValid)
        {
            var resultado = await _produtoService.AtualizarProduto(produto);

            if (resultado != null)
            {
                return RedirectToAction("Index");
            }
        }

        return View(produto);
    }

    [HttpGet]
    public async Task<IActionResult> Excluir(int id)
    {
        var produto = await _produtoService.ObterProdutoPorId(id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(ProdutoModel produto)
    {
        var resultado = await _produtoService.ExcluirProduto(produto.Id);

        if (resultado)
        {
            return RedirectToAction("Index");
        }

        return View(produto);
    }
}
