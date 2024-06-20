using Fitness.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<BodyWeight> BodyWeights { get; set; }
		public DbSet<Goal> Goal { get; set; }
		public DbSet<Meal> Meals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<BodyWeight>().HasData(
				new BodyWeight { Id = 1, Weight = 80.0, Unit = "kgs", Date = new DateTime(2020, 1, 1) },
				new BodyWeight { Id = 2, Weight = 75.0, Unit = "kgs", Date = new DateTime(2020, 1, 8) },
				new BodyWeight { Id = 3, Weight = 60.0, Unit = "kgs", Date = new DateTime(2020, 1, 22) }
			);
			modelBuilder.Entity<Goal>().HasData(
				new Goal { Id = 1, TargetWeight = 50.0, Unit = "kgs", TargetDate = new DateTime(2020, 2, 25), DailyCalories = 2000, DailyCarbs = 250, DailyProtein = 120, DailyFats = 50}
			);
			modelBuilder.Entity<Meal>().HasData(
				new Meal { Id = 1, Api_Id = 534358, FoodName = "Test", Servings = 1, ServingSizeAmount = 53, ServingSizeUnit = "g",
					Calories = 110.5, Carbohydrates = 15.6, Protein = 2, Fat = 9, MealTime = "Breakfast",
					Date = new DateOnly(2020, 1, 2), BrandName = "Test Brand inc."
				}
			);
		}
	}
}
