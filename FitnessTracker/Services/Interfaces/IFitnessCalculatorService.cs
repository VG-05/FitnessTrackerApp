using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services.Interfaces
{
	public interface IFitnessCalculatorService
	{
		Task<JObject?> GetNutritionInfoAsync(int age, string gender, int height, int weight, string activitylevel);
	}
}