using FieldGroove.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FieldGroove.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory httpClientFactory; 

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			this.httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public async Task<IActionResult> Leads()
		{
			var client = httpClientFactory.CreateClient();
			var response = await client.GetAsync("https://localhost:7222/api/Home/Leads");

			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();

				var dataModel = JsonConvert.DeserializeObject<List<LeadsModel>>(jsonData);

				return View(dataModel);
			}
			return View(new List<LeadsModel>());
		}

		[HttpGet]
		public IActionResult CreateLead()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateLead(LeadsModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var client = httpClientFactory.CreateClient();

			var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://localhost:7222/api/Home/CreateLead", jsonContent);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", "Failed to create lead");
			return RedirectToAction("Leads");
		}

		public IActionResult Dashboard()
		{
			return View();
		}

		public IActionResult EditLead()
		{
			return View();
		}
	}
}
