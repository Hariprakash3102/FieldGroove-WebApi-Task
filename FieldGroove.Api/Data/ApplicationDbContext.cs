using FieldGroove.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldGroove.Api.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<RegisterModel> UserData { get; set; }

		public DbSet<LeadsModel> Leads { get; set; }
	}
}
