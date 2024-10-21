using FieldGroove.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldGroove.Razor.Pages.Home
{
    public class DeleteLeadModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeleteLeadModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> OnDelete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            await httpClient.DeleteAsync($"api/Home/Delete/{id}");
            return RedirectToPage("/Home/Leads");
        }
    }
}