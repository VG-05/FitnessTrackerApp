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

		public List<SelectListItem>? ServingOptions { get; set; }

		/*public List<SelectListItem>? ServingOptions { get; set; } = Meal.ServingSizes.ForEach((serving) =>
        {
            new SelectListItem
            {
                Text = $"{serving.ServingName} ({serving.ServingAmount} {serving.ServingUnit})",
                Value = serving.ServingName
            };
        });*/
	}
}
