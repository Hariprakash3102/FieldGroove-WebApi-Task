using FieldGroove.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FieldGroove.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7222/api/Account/Login", model);
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
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7222/api/Account/Register", model);
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
