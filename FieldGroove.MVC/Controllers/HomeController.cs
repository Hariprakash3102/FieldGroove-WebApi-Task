using FieldGroove.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection;
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

		// Leads Action for HttpGet in MVC Controller

		[HttpGet]
        public async Task<IActionResult> Leads()
        {
            var client = httpClientFactory.CreateClient("FieldGrooveApi");
            var response = await client.GetFromJsonAsync<List<LeadsModel>>("Home/Leads");
            return View(response);
        }

		// CreateLead Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult CreateLead()
        {
            return View();
        }

		// CreateLead Action for HttpPost in MVC Controller

		[HttpPost]
        public async Task<IActionResult> CreateLead(LeadsModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("FieldGrooveApi");

                var response = await client.PostAsJsonAsync("Home/CreateLead", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Leads");
                }
            }
            return View(model);
        }

		// Dashboard Action for HttpGet in MVC Controller

		[HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

		// EditLead Action for HttpGet in MVC Controller

		[HttpGet]
        public async Task<IActionResult> EditLead(int id)
        {
            var client = httpClientFactory.CreateClient("FieldGrooveApi");
            var response = await client.GetFromJsonAsync<LeadsModel>($"Home/Leads/{id}");
            return View(response);
        }

		// EditLead Action for HttpPost in MVC Controller

		[HttpPost]
        public async Task<IActionResult> EditLead(LeadsModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("FieldGrooveApi");
                await client.PutAsJsonAsync<LeadsModel>("Home/EditLead", model);
                return RedirectToAction("Leads");
            }
            return View();
        }

		// DeleteLead Action for HttpGet in MVC Controller

		[HttpGet]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var client = httpClientFactory.CreateClient("FieldGrooveApi");
            await client.DeleteAsync($"Home/Delete/{id}");
            return RedirectToAction("Leads");
        }

		// Download the Csv file Action in MVC Controller

		[HttpGet]
        public async Task<IActionResult> DownloadCsv()
        {
            var client = httpClientFactory.CreateClient("FieldGrooveApi");
            var records = await client.GetFromJsonAsync<List<LeadsModel>>("Home/Leads");

            var csv = new StringBuilder();
            csv.AppendLine("ID,Project Name,Status,Added,Type,Contact,Action,Assignee,Bid Date");

            if(records is not  null)
            foreach (var record in records)
            {
                csv.AppendLine($"{record.Id},{record.ProjectName},{record.Status},{record.Added},{record.Type},{record.Contact},{record.Action},{record.Assignee},{record.BidDate}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", "LeadsData.csv");
        }

    }
}
