using FitnessTracker.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services
{
	public class FitnessCalculatorService : IFitnessCalculatorService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly string? _apiKey;
		private readonly HttpClient _httpClient;

        public FitnessCalculatorService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
			_httpClientFactory = httpClientFactory;
			_apiKey = configuration["RapidApiKey"];
			_httpClient = _httpClientFactory.CreateClient("caloriesapi");
		}
        public async Task<JObject?> GetNutritionInfoAsync(int age, string gender, int height, int weight, string activitylevel)
		{
			HttpRequestMessage request = new()
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{_httpClient.BaseAddress}/nutrition-info?measurement_units=met&age_type=yrs&age_value={age}&sex={gender}&cm={height}&kilos={weight}&activity_level={activitylevel}"),
				Headers =
				{
					{ "x-rapidapi-key", _apiKey },
					{ "X-RapidAPI-Host", "nutrition-calculator.p.rapidapi.com" }
				}
			};
			using HttpResponseMessage response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();
			string jsondata = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<JObject>(jsondata);
		}
	}
}