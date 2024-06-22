using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        public int Api_Id { get; set; }
        [DisplayName("Food")]
        public string? FoodName { get; set; } = string.Empty;
		[DisplayName("Brand")]
		public string? BrandName { get; set; } = string.Empty;
		public double? Servings { get; set; }
        public double? ServingSizeAmount { get; set; }
        public string? ServingSizeUnit { get; set; }
        public double? Calories { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }
        [AllowedValues("Breakfast", "Lunch", "Snacks", "Dinner")]
		[DisplayName("Meal Time")]
		public string MealTime { get; set; } = "Breakfast";         // "Breakfast", "Lunch", "Dinner", "Snacks"
        [Required]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
