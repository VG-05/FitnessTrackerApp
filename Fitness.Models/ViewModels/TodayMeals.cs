using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
    public class TodayMeals
    {
        public DateOnly Date {  get; set; }
        public double TotalCalories { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFats { get; set; }
        public double TotalProtein { get; set; }
    }
}
