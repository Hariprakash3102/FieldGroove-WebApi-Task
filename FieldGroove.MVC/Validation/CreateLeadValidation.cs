using FieldGroove.MVC.Models;
using FluentValidation;

namespace FieldGroove.MVC.Validation
{
	public class CreateLeadValidation : AbstractValidator<LeadsModel>
	{
		public CreateLeadValidation() 
		{
			RuleFor(x => x.ProjectName)
			   .NotEmpty().WithMessage("Project name is required.")
			   .Length(3, 100).WithMessage("Project name must be between 3 and 100 characters.");

			RuleFor(x => x.Contact)
				.NotEmpty().WithMessage("Phone Number is required")
				.LessThanOrEqualTo(9999999999).WithMessage("Contact Should be in 10 digit")
				.GreaterThanOrEqualTo(1000000000).WithMessage("Contact Should be in 10 digit");
		}
	}
}
