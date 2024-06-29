using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModels
{
	public class HomeVM
	{
		public double? LatestBodyweight { get; set; }
		public double? BodyweightProgress { get; set; }
		public double? OverallBodyweightProgress { get; set; }
		public string Unit { get; set; } = "kgs";
	}
}
