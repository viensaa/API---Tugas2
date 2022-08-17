using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _accountDAL;

        public AccountController(IAccount account)
        {
            _accountDAL = account;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Register()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Register(Account model)
        {
            try
            {
                var result = await _accountDAL.Insert(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
        }

        public IActionResult Login()
        {
            
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Account model)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            try
            {
                var result = await _accountDAL.Authenticate(model);
                var token = "bearer " + result.Token;
                var user = result.Username;
                //mengirimkan session token
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    HttpContext.Session.SetString("token", $"{token}");
                    
                }

                // TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button>  {token}</div>";
                ViewBag.user = result.Username;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

    }
}
