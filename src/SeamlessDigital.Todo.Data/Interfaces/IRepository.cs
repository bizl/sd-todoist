namespace SeamlessDigital.Todo.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public List<T> Get(T t);
        public int Insert(T t, Guid createUser);
        public int Update(T t, Guid updateUser);
    }
}
