using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;
using SeamlessDigital.Todo.Services.Interfaces;

namespace SeamlessDigital.Todo.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _repository;
        private readonly IWeatherService _weatherService;
        public TodoService(IRepository<TodoItem> repo, IWeatherService weatherService)
        {
            _repository = repo;
            _weatherService = weatherService;
        }
        public  IEnumerable<TodoItem> Fetch(TodoItem todoItem)
        {
            return _repository.Get(todoItem);
        }
    }
}
