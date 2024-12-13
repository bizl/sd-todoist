using SeamlessDigital.Todo.Domain;

namespace SeamlessDigital.Todo.Services.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoItem>> Fetch(TodoItem t);
    }
}
