using System.Diagnostics;
using loginpage.Models;
using loginpage.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace loginpage.Controllers
{
    public class HomeController : Controller
    {
        // Injects the database context and web host environment
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Hero()
        {
            return View();
        }

        // retrieves products from the database then display on Index view (homepage)
        //crud view
        public IActionResult Index()
        {
            var products = context.Products.OrderByDescending(p => p.Id).ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]

        public IActionResult Inventory()
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
