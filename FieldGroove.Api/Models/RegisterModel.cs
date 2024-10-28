using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Api.Models
{
	public class RegisterModel
	{
		[Display(Name = "First Name*")]
		public required string FirstName { get; set; }

        [Display(Name = "Last Name*")]
		public required string LastName { get; set; }

        [Display(Name = "Company Name*")]
		public required string CompanyName { get; set; }

        [Display(Name = "Phone*")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public long Phone { get; set; }

        [Key]
        [Display(Name = "Email*")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }

        [Display(Name = "Password Again*")]
        [Required(ErrorMessage = "Password again is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
		public required string PasswordAgain { get; set; }

        [Display(Name = "Time Zone*")]
		public required string TimeZone { get; set; }

        [Display(Name = "Street Address 1*")]
		public required string StreetAddress1 { get; set; }

        [Display(Name = "Street Address 2*")]
		public required string StreetAddress2 { get; set; }

        [Display(Name = "City*")]
		public required string City { get; set; }

        [Display(Name = "State*")]
		public required string State { get; set; }

        [Display(Name = "Zip*")]
		public required string Zip { get; set; }
	}
}
