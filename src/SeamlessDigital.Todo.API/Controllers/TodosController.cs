using Microsoft.AspNetCore.Mvc;
using SeamlessDigital.Todo.Domain;
using SeamlessDigital.Todo.Services.Interfaces;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;

namespace SeamlessDigital.Todo.API.Controllers
{
    [ApiController]
    [Route("api/todos")]
    //Manage your list of todos
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ITodoService _todoService;
        public TodosController(ILogger<TodosController> logger, ITodoService todoService, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _todoService = todoService;
        }

        [HttpGet]
        public async IAsyncEnumerable<TodoItem> Get(string? title, int? priority, string? dueDate)
        {
            var todoQry = new TodoItem { Priority = Convert.ToInt32(priority), Title = title };

            DateTime d;
            if (DateTime.TryParse(dueDate, out d))
            {
                todoQry.DueDate = d;
            }
            var todoItems =  _todoService.Fetch(todoQry);

            foreach (var item in todoItems)
            {
                yield return Weatherise(item); 
            }
        }

        private  TodoItem Weatherise(TodoItem item)
        {
            var weatherItem = _weatherService.Peek(item.Location).GetAwaiter().GetResult();
            if( weatherItem!=null )
            {
                item.Location.Weather = weatherItem;
            }
            return item;
        }
     
    }
}
