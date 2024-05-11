using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.DataAccess.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<BodyWeight> BodyWeights { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<BodyWeight>().HasData(
				new BodyWeight { Id = 1, Weight = 80, Unit = "kgs", Date = (new DateTime(2020, 1, 1)) },
				new BodyWeight { Id = 2, Weight = 75, Unit = "kgs", Date = new DateTime(2020, 1, 8) },
				new BodyWeight { Id = 3, Weight = 60, Unit = "kgs", Date = new DateTime(2020, 1, 22) }
				);
		}
	}
}
