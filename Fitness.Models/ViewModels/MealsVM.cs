using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
    public class MealsVM
    {
        public List<Meal>? Meals { get; set; }
        public string SearchString { get; set; } = string.Empty;
    }
}
