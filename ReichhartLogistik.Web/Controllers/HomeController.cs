using Microsoft.AspNetCore.Mvc;
using ReichhartLogistik.Models;
using ReichhartLogistik.Service;
using System.Diagnostics;

namespace ReichhartLogistik.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _recipeService;
        public HomeController(ILogger<HomeController> logger,
            IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}