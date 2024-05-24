using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Fitness.Models.ViewModels
{
	public class DayMealVM
	{
		public DateOnly Date {  get; set; } = DateOnly.FromDateTime(DateTime.Now);
		public List<Meal> Breakfast { get; set; } = [];
		public List<Meal> Lunch { get; set; } = [];
		public List<Meal> Snacks { get; set; } = [];
		public List<Meal> Dinner { get; set; } = [];
	}
}
