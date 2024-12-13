using Microsoft.AspNetCore.Authentication.OAuth.Claims;
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
        const int DEFAULT_USR = 11;
        private readonly ILogger<TodosController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ITodoService _todoService;
        public TodosController(ILogger<TodosController> logger, ITodoService todoService, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _todoService = todoService;
        }

        /// <summary>
        /// List of Todo items in database. Filter by title, priority or due date with parameters 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="priority"></param>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        [HttpGet]
        public async IAsyncEnumerable<TodoItem> Get(string? title, int? priority, string? dueDate)
        {
            var todoQry = new TodoItem { };
            if (!string.IsNullOrEmpty(title))
            {
                todoQry.Title = title;
            }
            if (priority!=null)
            {
                todoQry.Priority = (int)priority;
            }
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
            if (item.Location?.Latitude != null && item.Location?.Longitude != null)
            {
                var weatherItem = _weatherService.Peek(item.Location).GetAwaiter().GetResult();
                if (weatherItem != null)
                {
                    item.Location.Weather = weatherItem;
                }
            }
            return item;
        }

        [HttpPut]
        public async Task<IActionResult> CreateTodoItem ([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrEmpty(todoItem.Title))
            {
                return BadRequest();
            }

            await  _todoService.Insert(todoItem, DEFAULT_USR); 

            return CreatedAtAction(nameof(CreateTodoItem), new { id = todoItem.Id }, todoItem);

        }


        [HttpPost]
        public async Task<IActionResult> UpdateTodoItem([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrEmpty(todoItem.Title))
            {
                return BadRequest();
            }

            await _todoService.Update(todoItem, DEFAULT_USR);

            return CreatedAtAction(nameof(UpdateTodoItem), new { id = todoItem.Id }, todoItem);

        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        { 
            await _todoService.Delete(id, DEFAULT_USR);

            return Ok(nameof(DeleteTodoItem));

        }

    }
}
