using SeamlessDigital.Todo.Domain;
using SeamlessDigital.Todo.Services.Interfaces;
using System.Net.Http.Json;

namespace SeamlessDigital.Todo.Services
{
    public class WeatherService : IWeatherService
    {
        private string _serviceEndpoint;
        private string _apiKey; 
        private readonly HttpClient _client;
        public WeatherService(string serviceEndpoint, string apiKey)
        {
            _serviceEndpoint = serviceEndpoint;
            _apiKey = apiKey; 
            _client = new HttpClient(); 
        }

        public async Task<WeatherItem> Peek(Location location)
        {
            var url = string.Format(_serviceEndpoint, _apiKey, $"{location?.Latitude},{location?.Longitude}");

            var request = new HttpRequestMessage(HttpMethod.Post, url);


            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var t = await response.Content.ReadFromJsonAsync<WeatherItem>();

            return t;
        }
    }
}
