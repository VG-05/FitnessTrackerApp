using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string Unit { get; set; } = string.Empty;          
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime Date { get; set; }
		[Required]
		public string? UserID { get; set; }
		[ForeignKey("UserID")]
		public ApplicationUser? User { get; set; }
	}
}
