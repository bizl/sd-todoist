using SeamlessDigital.Todo.Domain;

namespace SeamlessDigital.Todo.Services.Interfaces
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> Fetch(TodoItem todoItem);

        Task<int> Insert(TodoItem todoITem, int createUser);
        Task<int> Update(TodoItem todoITem, int updateUser);
        Task<int> Delete(int id, int deleteUser);
    }
}
