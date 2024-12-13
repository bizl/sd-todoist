using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeamlessDigital.Todo.Domain
{
    public class TodoItem
    { 
        public int UserId { get; set; }
        public int Id { get; set; }
        public bool Completed { get; set; }

        public string? Todo { get; set; }
        public Category?  Category { get; set; }

        [Range(1,5)]
        [DefaultValue(3)]
        public int Priority { get; set; }

        public Location? Location { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
