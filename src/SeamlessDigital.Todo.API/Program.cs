using Microsoft.Extensions.DependencyInjection;
using SeamlessDigital.Todo.Data;
using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;
using SeamlessDigital.Todo.Services;
using SeamlessDigital.Todo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var weatherServiceUrl = builder.Configuration.GetValue<string>("WeatherServiceSettings:WeatherServiceUrl");
var apiKey = builder.Configuration.GetValue<string>("WeatherServiceSettings:apiKey");
builder.Services.AddScoped<IRepository<TodoItem>, TodoRepository>(x => new TodoRepository(connectionString));
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>(x => new CategoryRepository(connectionString));
builder.Services.AddScoped<IWeatherService, WeatherService>(x => new WeatherService(weatherServiceUrl, apiKey));
builder.Services.AddScoped<ITodoService, TodoService>(x => new TodoService(new TodoRepository(connectionString), new WeatherService(weatherServiceUrl, apiKey), new CategoryRepository(connectionString)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
