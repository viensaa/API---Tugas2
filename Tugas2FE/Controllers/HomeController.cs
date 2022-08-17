using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tugas2FE.Models;

namespace Tugas2FE.Controllers
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
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                return RedirectToAction("Login", "Account");
            }
            ViewData["user"] = TempData["user"] ?? TempData["user"];
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