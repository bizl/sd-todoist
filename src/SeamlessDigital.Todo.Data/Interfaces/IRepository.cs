namespace SeamlessDigital.Todo.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
         List<T> Get(T t);
        Task<int> Insert(T t, int createUser);
        Task<int> Update(T t, int updateUser);
        Task<int> Delete(int id, int updateUser);
    }
}
