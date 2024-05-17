using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public string? FoodName { get; set; } = string.Empty;
        public double? ServingSize { get; set; }
        public double? Calories { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }
        public string? MealTime { get; set; } = string.Empty;         // "Breakfast", "Lunch", "Dinner", "Snacks"
        public DateOnly Date { get; set; }
        public string? BrandName { get; set; } = string.Empty;
    }
}
