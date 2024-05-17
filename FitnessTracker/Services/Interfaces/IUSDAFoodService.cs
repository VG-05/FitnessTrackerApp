using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services.Interfaces
{
	public interface IUSDAFoodService
	{
		public JObject GetFoodDataAsync(string query);
		public JObject GetFoodDataByIdAsync(int id);
	}
}
