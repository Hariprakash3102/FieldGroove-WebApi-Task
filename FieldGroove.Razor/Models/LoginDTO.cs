using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Razor.Models
{
	public class LoginDTO
	{
		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		[Display(Name = "Remember Me")]
		public bool RemenberMe { get; set; }
	}
}
