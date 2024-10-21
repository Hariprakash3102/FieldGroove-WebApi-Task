using FieldGroove.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldGroove.Razor.Pages.Home
{
    public class CreateLeadModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CreateLeadModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

		}

        [BindProperty]
        public LeadsDTO CreateLead { get; set; } = new LeadsDTO();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() {
            if (CreateLead != null) {
				var httpClient = _httpClientFactory.CreateClient("FieldGrooveApi");
                await httpClient.PostAsJsonAsync<LeadsDTO>("Home/CreateLead",CreateLead);
                return RedirectToPage("/Home/Leads");
			}
            return Page();
        }
    }
}
