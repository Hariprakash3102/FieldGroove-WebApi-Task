using System.ComponentModel.DataAnnotations;

namespace FieldGroove.Api.Models
{
	public class RegisterModel
	{
		[Required]
		[Display(Name = "First Name*")]
		public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name*")]
		public string? LastName { get; set; }

        [Required]
        [Display(Name = "Company Name*")]
		public string? CompanyName { get; set; }

        [Required]
        [Display(Name = "Phone*")]
		public long Phone { get; set; }

        [Required]
        [Key]
		[Display(Name = "Email*")]
		public string? Email { get; set; }

        [Required]
        [Display(Name = "Password*")]
		public string? Password { get; set; }

        [Required]
        [Display(Name = "Password Again*")]
		public string? PasswordAgain { get; set; }

        [Required]
        [Display(Name = "Time Zone*")]
		public string? TimeZone { get; set; }

        [Required]
        [Display(Name = "Street Address 1*")]
		public string? StreetAddress1 { get; set; }

        [Required]
        [Display(Name = "Street Address 2*")]
		public string? StreetAddress2 { get; set; }

        [Required]
        [Display(Name = "City*")]
		public string? City { get; set; }

        [Required]
        [Display(Name = "State*")]
		public string? State { get; set; }

        [Required]
        [Display(Name = "Zip*")]
		public string? Zip { get; set; }
	}
}
