using System.ComponentModel.DataAnnotations;

namespace Fitness.Models
{
	public class BodyWeight
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public double Weight { get; set; }
		[AllowedValues(["kgs", "lbs"], ErrorMessage = "Please enter one of 'kgs' or 'lbs'.")]
		[Required]
		public string Unit { get; set; }             
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime Date { get; set; }
	}
}
