using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.Data;
using WebShop.Models;

namespace ASPNetCore_Modeldata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Producten(Categorie? categorie)
        {
            if (categorie == null)
            {
                return View();
            }

            var productenFiltered = ProductData.Producten.Where(p => p.Categorie == categorie)
                                                         .OrderBy(p => p.Naam);
            return View(productenFiltered);
        }

        public IActionResult Openingsuren()
        {
            DateTime nu = DateTime.Now;
            bool open = false;

            if (nu.DayOfWeek != DayOfWeek.Sunday)
            {
                if (nu.DayOfWeek != DayOfWeek.Saturday)
                {
                    if (nu.Hour >= 8 && nu.Hour < 20)
                    {
                        open = true;
                    }
                }
                else if (nu.Hour >= 10 && nu.Hour < 22)
                {
                    open = true;
                }
            }

            ViewBag.Open = open;

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