using SeamlessDigital.Todo.Domain;

namespace SeamlessDigital.Todo.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherItem> Peek(Location location); 
    }
}
