using System.ComponentModel.DataAnnotations;

namespace FieldGroove.MVC.Models
{
	public class LoginModel
	{
		public string? Email { get; set; }

		public string? Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RemenberMe { get; set; }
	}
}
