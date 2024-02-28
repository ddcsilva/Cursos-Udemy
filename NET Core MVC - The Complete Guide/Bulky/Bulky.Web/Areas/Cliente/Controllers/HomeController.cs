using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bulky.Web.Areas.Cliente.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar a área do cliente.
    /// </summary>
    [Area("Cliente")]
    public class HomeController : Controller
    {
        // ILogger é uma interface que permite a gravação de logs.
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Action responsável por exibir a página inicial.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action responsável por exibir a página de erro.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
