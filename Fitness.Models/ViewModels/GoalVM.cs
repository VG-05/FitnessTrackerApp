using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
    public class GoalVM
    {
        public Goal Goal { get; set; } = new()
		{
			TargetDate = DateTime.Now.AtMidnight()
		};
        [ValidateNever]
        public IEnumerable<SelectListItem> Units { get; set; } = [
			new SelectListItem { Text = "kgs" , Value = "kgs"},
			new SelectListItem { Text = "lbs" , Value = "lbs"}
		];
		public string? CaloriesDesc { get; set; } = string.Empty;
		public string? CarbsDesc { get; set; } = string.Empty;
		public string? ProteinDesc { get; set; } = string.Empty;
		public string? FatsDesc { get; set; } = string.Empty;
	}
}
