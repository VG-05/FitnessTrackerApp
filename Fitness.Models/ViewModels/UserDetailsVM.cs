using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
	public class UserDetailsVM
	{
		public int Id { get; set; }
		public int Age { get; set; }
		[AllowedValues("male", "female")]
		public string Sex { get; set; } = string.Empty;
		public int Height { get; set; }       // in cm
		public int Weight { get; set; }       // in kg

		[AllowedValues("Inactve", "Low Active", "Active", "Very Active")]
		public string ActivityLevel { get; set; } = string.Empty;
	}
}
