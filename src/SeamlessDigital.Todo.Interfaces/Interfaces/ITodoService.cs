using SeamlessDigital.Todo.Domain;

namespace SeamlessDigital.Todo.Services.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> Fetch(TodoItem t);
    }
}
