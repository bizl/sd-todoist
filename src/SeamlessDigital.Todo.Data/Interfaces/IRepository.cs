namespace SeamlessDigital.Todo.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
         List<T> Get(T t);
         int Insert(T t, int createUser);
         int Update(T t, int updateUser); 
    }
}
