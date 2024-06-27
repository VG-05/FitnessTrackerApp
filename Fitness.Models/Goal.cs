using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Target Weight")]
        public double TargetWeight { get; set; }
        [AllowedValues(["kgs", "lbs"], ErrorMessage = "Please enter one of 'kgs' or 'lbs'.")]
        [Required]
        public string Unit { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Target Date")]
        public DateTime TargetDate { get; set; }
        [DisplayName("Daily Calories")]
        public int DailyCalories { get; set; }
        [DisplayName("Daily Carbohydrates")]
        public int DailyCarbs { get; set; }
        [DisplayName("Daily Protein")]
        public int DailyProtein { get; set; }
        [DisplayName("Daily Fats")]
        public int DailyFats { get; set; }
		[Required]
		public string? UserID { get; set; }
		[ForeignKey("UserID")]
		public ApplicationUser? User { get; set; }
	}
}
