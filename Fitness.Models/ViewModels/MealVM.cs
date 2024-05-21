using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
	public class MealVM
	{
		public Meal Meal { get; set; } = new Meal();
        public List<SelectListItem>? MealTimes { get; set; } = new()
        {
            new SelectListItem
            {
                Value = "Breakfast",
                Text = "Breakfast"
            },
            new SelectListItem
            {
                Value = "Lunch",
                Text = "Lunch"
            },
            new SelectListItem
            {
                Value = "Snacks",
                Text = "Snacks"
            },
            new SelectListItem
            {
                Value = "Dinner",
                Text = "Dinner"
            }
        };
	}
}
