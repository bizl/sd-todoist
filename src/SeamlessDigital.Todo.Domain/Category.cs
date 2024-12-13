namespace SeamlessDigital.Todo.Domain
{
    public class Category
    {
        public string Title { get; set; }
        internal Guid Parent { get; set; }
    }
}
