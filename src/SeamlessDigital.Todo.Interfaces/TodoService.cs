using SeamlessDigital.Todo.Data.Interfaces;
using SeamlessDigital.Todo.Domain;
using SeamlessDigital.Todo.Services.Interfaces;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace SeamlessDigital.Todo.Services
{
    public class TodoService : ITodoService
    {

        private readonly IRepository<TodoItem> _todoItemsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IWeatherService _weatherService;
        public TodoService(IRepository<TodoItem> todoRepo, IWeatherService weatherService, IRepository<Category> caetegoriesRepo)
        {
            _todoItemsRepository = todoRepo;
            _categoriesRepository = caetegoriesRepo;
            _weatherService = weatherService;
        }

        public  IEnumerable<TodoItem> Fetch(TodoItem todoItem)
        {
            return _todoItemsRepository.Get(todoItem);
        }

        public async Task<int> Insert(TodoItem todoItem, int createUser)
        {
            var theTargetCategory = _categoriesRepository.Get(new Category { Title = todoItem.Category }).FirstOrDefault();
            if (theTargetCategory != null)
            {
                todoItem.Category = theTargetCategory?.Key.ToString();
            }
            return await _todoItemsRepository.Insert(todoItem, createUser);
        }

        public async Task<int> Update(TodoItem todoItem, int updateUser)
        {
            return await _todoItemsRepository.Update(todoItem, updateUser);
        }

        public async Task<int> Delete(int itemId, int deleteUser)
        {
            return await _todoItemsRepository.Delete(  itemId, deleteUser);
        }
    }
}
