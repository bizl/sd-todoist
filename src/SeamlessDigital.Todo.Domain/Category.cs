namespace SeamlessDigital.Todo.Domain
{
    public class Category
    {
        public Guid Key { get; set; }
        public string? Title { get; set; } 
        public Guid Parent { get; set; }
    }
}
