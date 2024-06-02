using FitnessTracker.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services
{
    public class USDAFoodService : IUSDAFoodService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string? _apiKey;
        private readonly HttpClient _httpClient;
		public USDAFoodService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["USDAApiKey"];
			_httpClient = _httpClientFactory.CreateClient("nutritionapi");
	    }

	    public async Task<JObject> GetFoodDataAsync(string query)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/foods/search?query={query}&api_key={_apiKey}&pageSize=10");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JObject.Parse(data);
        }
        public async Task<JObject> GetFoodDataByIdAsync(int id)
        {
			using HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/food/{id}?api_key={_apiKey}&nutrients=203,204,205,208&format=full");
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            return JObject.Parse(data);
        }
    }
}