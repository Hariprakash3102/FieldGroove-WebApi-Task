using FieldGroove.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldGroove.Razor.Pages.Home
{
	public class LeadsModel : PageModel
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public LeadsModel(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[BindProperty]
		public List<LeadsDTO> Leads { get; set; } = new List<LeadsDTO>();

		public async Task OnGet()
		{
			var httpClient = _httpClientFactory.CreateClient("FieldGrooveApi");
			var result = await httpClient.GetFromJsonAsync<List<LeadsDTO>>("Home/Leads");
			Leads = result ?? new List<LeadsDTO>();
		}

        public async Task<IActionResult> OnPost(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("FieldGrooveApi");
            await httpClient.DeleteAsync($"Home/Delete/{id}");
            return RedirectToPage("/Home/Leads");
        }
    }
}
