using FieldGroove.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldGroove.Razor.Pages.Home
{
    public class EditLeadModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditLeadModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LeadsDTO EditLead { get; set; } = new LeadsDTO();

        public async Task OnGet(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("FieldGrooveApi");
            var result = await httpClient.GetFromJsonAsync<LeadsDTO>($"Home/Leads/{id}");
            EditLead = result ?? new LeadsDTO();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("FieldGrooveApi");
                await httpClient.PutAsJsonAsync<LeadsDTO>($"Home/EditLead",EditLead);
                return RedirectToPage("/Home/Leads");
            }
            return Page();
        }
    }
}
