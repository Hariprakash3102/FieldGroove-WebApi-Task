
using FieldGroove.MVC.Models;
using FluentValidation;

namespace FieldGroove.MVC.Validation
{
	public class LoginValidator : AbstractValidator<LoginModel>
	{
		public LoginValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is Required")
				.EmailAddress().WithMessage("Invalid Email Address");
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is Required");
		}
	}
}
