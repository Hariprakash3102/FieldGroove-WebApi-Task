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

			//RuleFor(x => x.Status)
			//	.NotEmpty().WithMessage("Select the Status.");

			//RuleFor(x => x.Added)
			//	.NotEmpty().WithMessage("Added date is required.")
			//	.GreaterThanOrEqualTo(DateTime.Now).WithMessage("Added date in the present.");

			//RuleFor(x => x.Type)
			//	.NotNull().WithMessage("Type must be specified.");

			RuleFor(x => x.Contact)
				.NotEmpty().WithMessage("Phone Number is required")
				.LessThanOrEqualTo(9999999999).WithMessage("Contact Should be in 10 digit")
				.GreaterThanOrEqualTo(1000000000).WithMessage("Contact Should be in 10 digit");

			//RuleFor(x => x.Action)
			//	.NotEmpty().WithMessage("Action is required.")
			//	.MaximumLength(250).WithMessage("Action can have a maximum of 250 characters.");

			//RuleFor(x => x.Assignee)
			//	.NotEmpty().WithMessage("Select the Assignee.")
			//	.Length(2, 50).WithMessage("Assignee name must be between 2 and 50 characters.");

			//RuleFor(x => x.BidDate)
			//	.NotEmpty().WithMessage("Bid Date is required.")
			//	.GreaterThanOrEqualTo(DateTime.Now).WithMessage("Bid date cannot be in the past.");
		}
	}
}
