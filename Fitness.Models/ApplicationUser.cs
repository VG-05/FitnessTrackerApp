using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        public string Name { get; set; } = string.Empty;
        [PersonalData]
        public int Height { get; set; }
        [PersonalData]
        [AllowedValues("male", "female")]
        public string Gender { get; set; } = "male";
        [PersonalData]
        [Range(1, 150)]
        public int Age { get; set; }
        [PersonalData]
        [AllowedValues("Inactive", "Low Active", "Active", "Very Active")]
        public string ActivityLevel { get; set; } = string.Empty;

        public ICollection<Meal> Meals { get; set; } = [];
        public ICollection<BodyWeight> BodyWeights { get; set; } = [];
		public ICollection<Goal> Goals { get; set; } = [];
	}
}
