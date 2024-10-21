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

        [HttpGet]
        public async Task<IActionResult> Leads()
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<List<LeadsModel>>("https://localhost:7222/api/Home/Leads");
            return View(response);
        }

        [HttpGet]
        public IActionResult CreateLead()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead(LeadsModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7222/api/Home/CreateLead", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Leads");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditLead(int id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<LeadsModel>($"https://localhost:7222/api/Home/Leads/{id}");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> EditLead(LeadsModel model)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient();
                await client.PutAsJsonAsync<LeadsModel>("https://localhost:7222/api/Home/EditLead", model);
                return RedirectToAction("Leads");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7222/api/Home/Delete/{id}");
            return RedirectToAction("Leads");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCsv()
        {
            var client = httpClientFactory.CreateClient();
            var records = await client.GetFromJsonAsync<List<LeadsModel>>("https://localhost:7222/api/Home/Leads");

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
