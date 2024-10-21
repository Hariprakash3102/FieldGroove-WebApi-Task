using System.ComponentModel.DataAnnotations;

namespace FieldGroove.MVC.Models
{
	public class RegisterModel
	{

		[Display(Name = "First Name*")]
		public string? FirstName { get; set; }

		[Display(Name = "Last Name*")]
		public string? LastName { get; set; }

		[Display(Name = "Company Name*")]
		public string? CompanyName { get; set; }

		[Display(Name = "Phone*")]
		public long Phone { get; set; }

		[Key]
		[Display(Name = "Email*")]
		public string? Email { get; set; }

		[Display(Name = "Password*")]
		public string? Password { get; set; }

		[Display(Name = "Password Again*")]
		public string? PasswordAgain { get; set; }

		[Display(Name = "Time Zone*")]
		public string? TimeZone { get; set; }

		[Display(Name = "Street Address 1*")]
		public string? StreetAddress1 { get; set; }

		[Display(Name = "Street Address 2*")]
		public string? StreetAddress2 { get; set; }

		[Display(Name = "City*")]
		public string? City { get; set; }

		[Display(Name = "State*")]
		public string? State { get; set; }

		[Display(Name = "Zip*")]
		public string? Zip { get; set; }
	}
}
