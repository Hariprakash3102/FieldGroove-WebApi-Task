using System.ComponentModel.DataAnnotations;

namespace FieldGroove.MVC.Models
{
	public class LeadsModel
	{

		[Key]
		public int? Id { get; set; }

		public string? ProjectName { get; set; }

		public string? Status { get; set; }

		public DateTime? Added { get; set; } = DateTime.Now;

		public bool Type { get; set; }

		public long? Contact { get; set; }

		public string? Action { get; set; }

		public string? Assignee { get; set; }

		public DateTime? BidDate { get; set; }

	}
}
