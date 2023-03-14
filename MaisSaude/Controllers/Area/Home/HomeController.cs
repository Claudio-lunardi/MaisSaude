using MaisSaude.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace MaisSaude.Controllers.Area.Home
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
        public ActionResult EditarDadosCadastrais()
        {
            var TypeClient = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
            var ID = User.Claims.Where(c => c.Type == "ID").Select(c => c.Value).FirstOrDefault();

            if ("titular" == TypeClient)
            {
                return RedirectToAction("Edit", "Titular", new { area = "", ID = ID });
            }
            else if ("dependente" == TypeClient)
            {
                return RedirectToAction("Edit", "Dependente", new { area = "", ID = ID });
            }
            else if ("clinica" == TypeClient)
            {
                return RedirectToAction("Edit", "Clinica", new { area = "", ID = ID });

            }
            else
            {
                return View();
            }



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