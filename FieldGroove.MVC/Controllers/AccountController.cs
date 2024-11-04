using FieldGroove.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FieldGroove.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

		// Index Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

		// Login Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		// Login Action for HttpPost in MVC Controller

		[HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("FieldGrooveApi");
                var response = await client.PostAsJsonAsync("Account/Login", model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Dashboard", "Home");
            }
            return View(model);
        }

		// Register Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult Register()
        {
            return View();
		}

        // Register Action for HttpPost in MVC Controller

		[HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // Assert

            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("FieldGrooveApi");
                var response = await client.PostAsJsonAsync("Account/Register", model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("WaitingActivation", "Account");
            }
            return View(model);
        }

		// WaitingActivation Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult WaitingActivation()
        {
            return View();
        }
    }
}
