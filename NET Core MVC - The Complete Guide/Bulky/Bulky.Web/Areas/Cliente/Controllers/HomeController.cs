using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bulky.Web.Areas.Cliente.Controllers
{
    /// <summary>
    /// Controller respons�vel por gerenciar a �rea do cliente.
    /// </summary>
    [Area("Cliente")]
    public class HomeController : Controller
    {
        // ILogger � uma interface que permite a grava��o de logs.
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Action respons�vel por exibir a p�gina inicial.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action respons�vel por exibir a p�gina de erro.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
