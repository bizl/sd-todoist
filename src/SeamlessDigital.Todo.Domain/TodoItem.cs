using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeamlessDigital.Todo.Domain
{
    public class TodoItem
    { 
        [Required]
        public int UserId { get; set; }
         
        public int Id { get;  set; }
        public bool Completed { get; set; }

        [Required]
        public string Todo { get; set; }
        public string?  Category { get; set; }

        [Range(1,5)] 
        public int Priority { get; set; }

        public Location? Location { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
