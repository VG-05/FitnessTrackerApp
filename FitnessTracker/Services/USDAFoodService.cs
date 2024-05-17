using FitnessTracker.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace FitnessTracker.Services
{
    public class USDAFoodService : IUSDAFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public USDAFoodService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.nal.usda.gov/fdc/v1");
            _apiKey = configuration["USDAApiKey"];
        }

        public JObject GetFoodDataAsync(string query)
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/foods/search?query={query}&api_key={_apiKey}&pageSize=10").Result;
            response.EnsureSuccessStatusCode();

            string data = response.Content.ReadAsStringAsync().Result;
            return JObject.Parse(data);
        }
        public JObject GetFoodDataByIdAsync(int id)
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/food/{id}?api_key={_apiKey}&nutrients=203,204,205,208&format=abridged").Result;
            response.EnsureSuccessStatusCode();

            string data = response.Content.ReadAsStringAsync().Result;
            return JObject.Parse(data);
        }
    }
}