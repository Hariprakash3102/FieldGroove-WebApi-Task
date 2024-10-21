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

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7222/api/Account/Register", model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("WaitingActivation", "Account");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult WaitingActivation()
        {
            return View();
        }
    }
}
