using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services.Interfaces
{
	public interface IUSDAFoodService
	{
		Task<JObject> GetFoodDataAsync(string query);
		Task<JObject> GetFoodDataByIdAsync(int id);
	}
}
